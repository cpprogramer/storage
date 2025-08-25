using Common.Messages;
using Common.UI.Messages;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using CWindow = Common.UI.IUIViewModel< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public sealed class UiManager : IUiManager< Type, WindowResult >, IUserActionsQueue, IDisposable
    {
        private readonly Dictionary< Type, CWindow > _openedWindows = new();
        private readonly Dictionary< Type, Func< CWindow > > _registeredWindows = new();
        private readonly Queue< IMessage > _queueMessages = new();
        private readonly IMessageBroker _messageBroker;
        private readonly CompositeDisposable _disposables;
        private CWindow _activeModal;

        public UiManager( IMessageBroker messageBroker )
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            _disposables = new CompositeDisposable();

            //TODO R3
            Observable.Timer( TimeSpan.FromSeconds( 1 ) ).Repeat().Subscribe( _ => { TickHandle(); } )
                .AddTo( _disposables );
            _messageBroker.Receive< UIOpenWindowMessage >().Subscribe( UIOpenWindowMessageHandler )
                .AddTo( _disposables );
            _messageBroker.Receive< UICloseWindowMessage >().Subscribe( UICloseWindowMessageHandler )
                .AddTo( _disposables );
        }

        public void AddRequest( IMessage message ) => _queueMessages.Enqueue( message );

        public void Dispose() => _disposables.Dispose();

        public void RegisterWindow( Type type, Func< CWindow > func ) => _registeredWindows[ type ] = func;

        public bool IsOpened( Func< string, bool > condition )
        {
            foreach ( KeyValuePair< Type, CWindow > kv in _openedWindows )
                if ( condition( kv.Value.TypeWindow.Name ) )
                    return true;

            return false;
        }

        private void UICloseWindowMessageHandler( UICloseWindowMessage args ) =>
            CloseWindow( args.WindowType, args.WindowResult );

        private void UIOpenWindowMessageHandler( UIOpenWindowMessage msg ) => ShowWindow( msg );

        private void TickHandle()
        {
            if ( _queueMessages.Count > 0 )
            {
                IMessage msg = _queueMessages.Dequeue();
                _messageBroker.Publish( msg );
                _queueMessages.Clear();
            }
        }

        private void ClosingFinishedHandler( CWindow iuiViewModel, WindowResult windowResult )
        {
            if ( iuiViewModel == null ) return;

            iuiViewModel.OnClosingFinished -= ClosingFinishedHandler;

            if ( _openedWindows.ContainsKey( iuiViewModel.TypeWindow ) )
                _openedWindows.Remove( iuiViewModel.TypeWindow );

            if ( iuiViewModel == _activeModal ) _activeModal = null;
        }

        private void ShowWindow( UIOpenWindowMessage msg )
        {
            if ( _openedWindows.ContainsKey( msg.Dto.TypeWindow ) ) return;

            CWindow opened = CreateWindowInstance( msg.Dto.TypeWindow );
            opened.OnClosingFinished += ClosingFinishedHandler;
            opened.Initialize( msg.Dto );
            _openedWindows.Add( opened.TypeWindow, opened );
        }

        private void CloseWindow( Type typeWindow, WindowResult result )
        {
            if ( _openedWindows.TryGetValue( typeWindow, out CWindow wnd ) )
            {
                wnd.Close( result );
                if ( wnd == _activeModal ) _activeModal = null;
            }
        }

        private CWindow CreateWindowInstance( Type typeWindow )
        {
            if ( !_registeredWindows.TryGetValue( typeWindow, out Func< CWindow > func ) )
                throw new UnityException( $"Invalid key : {typeWindow} Add window to Manager" );

            return func.Invoke();
        }
    }
}