using Common.UI.Messages;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using CWindow = Common.UI.IWindow< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public sealed class UiManager : IUiManager< Type, WindowResult >, IUserActionsQueue, IDisposable
    {
        private readonly Dictionary< Type, CWindow > _openedWindows = new();
        private readonly Dictionary< Type, Func<CWindow> > _registeredWindows = new();
        
        private readonly Queue< IBaseMessage > _queueMessages = new();
      
        private readonly IMessageBroker _messageBroker;
        private readonly CompositeDisposable _disposables;
        private CWindow _activeModal;

        public UiManager( IMessageBroker messageBroker )
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            _disposables = new CompositeDisposable();
            
            //R3
            Observable.Timer( System.TimeSpan.FromSeconds( 1 ) ).Repeat().Subscribe( _ => { TickHandle(); } )
                .AddTo( _disposables );
           
            _messageBroker.Receive< UIOpenWindowMessage >().Subscribe( UIOpenWindowMessageHandler )
                .AddTo( _disposables );
            _messageBroker.Receive< UICloseWindowMessage >().Subscribe( UICloseWindowMessageHandler )
                .AddTo( _disposables );
        }

        private void UICloseWindowMessageHandler( UICloseWindowMessage args ) 
            => CloseWindow( args.WindowType, args.WindowResult );

        private void TickHandle()
        {
            if ( _queueMessages.Count > 0 )
            {
                IBaseMessage msg = _queueMessages.Dequeue();
                _messageBroker.Publish( msg );
                _queueMessages.Clear();
            }
        }

        private void ClosingFinishedHandler( CWindow window, WindowResult windowResult )
        {
            if ( window == null ) return;

            window.OnClosingFinished -= ClosingFinishedHandler;
            
            if ( _openedWindows.ContainsKey( window.TypeWindow ) )
            {
                _openedWindows.Remove( window.TypeWindow );
            }

            if ( window == _activeModal ) _activeModal = null;
        }

        private void UIOpenWindowMessageHandler(UIOpenWindowMessage msg)
        {
            ShowWindow(msg);
        }
        
        public void AddRequest( IBaseMessage message ) => _queueMessages.Enqueue( message );

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public bool IsOpened( Func< string, bool > condition )
        {
            foreach ( var kv in _openedWindows )
                if ( condition( kv.Value.TypeWindow.Name ) )
                    return true;

            return false;
        }

        private void ShowWindow( UIOpenWindowMessage msg )
        {
            if ( _openedWindows.ContainsKey( msg.Model.TypeWindow ) ) return;
           
            var opened = CreateWindowInstance( msg.Model.TypeWindow );
            opened.OnClosingFinished += ClosingFinishedHandler;
            opened.Initialize(msg.Model);
            _openedWindows.Add( opened.TypeWindow, opened );
        }

        public void RegisterWindow( Type type, Func<CWindow> func )
        {
            _registeredWindows[ type ] = func;
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
            if ( !_registeredWindows.TryGetValue( typeWindow, out var func ) )
                throw new UnityException( $"Invalid key : {typeWindow} Add window to Manager" );

            return func.Invoke();
        }
    }
}