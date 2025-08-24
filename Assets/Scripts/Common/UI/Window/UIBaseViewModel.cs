using Common.Messages;
using Common.UI.Model;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UI;
using UnityEngine;
using UIViewModel = Common.UI.IUIViewModel< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public abstract class UIBaseViewModel< TView > : UIViewModel
        where TView : MonoBehaviour, IBaseView
    {
        public event Action< UIViewModel, WindowResult > OnClosingFinished;
        public event Action< UIViewModel > OnClosingStarted;

        public Type TypeWindow => GetType();
        public string WindowName { get; private set; }

        protected TView _baseView;

        protected readonly IUiRootAggregator _uiRootAggregator;
        private readonly IResourcesProvider _resourcesProvider;

        protected UIBaseViewModel( IUiRootAggregator uiRootAggregator )
        {
            _uiRootAggregator = uiRootAggregator ?? throw new ArgumentNullException( nameof(uiRootAggregator) );
            _resourcesProvider = new ResourcesProvider();
        }

        protected abstract void OnInitialize( BaseWindowDTO dto );

        protected virtual void OnClosing()
        {
        }

        public void Close( WindowResult result = WindowResult.Undefined )
        {
            OnClosingStarted?.Invoke( this );

            if ( _baseView != default )
            {
                _baseView.CloseView();

                if ( _baseView.IsDestroyable ) _baseView = default;
            }

            _resourcesProvider.Release( WindowName );
            OnClosingFinished?.Invoke( this, result );
        }

        public void Initialize( BaseWindowDTO baseDto )
        {
            WindowName = baseDto.WindowName;
            InitializeAsync( baseDto ).Forget();
        }

        protected void AddRequest(IMessage message)
        {
            _uiRootAggregator.UserActionsQueue.AddRequest( message );
        }
        
        private async UniTaskVoid InitializeAsync( BaseWindowDTO baseDto )
        {
            try
            {
                var view = await _resourcesProvider.LoadResourceAsync< GameObject >( baseDto.WindowName );
                _baseView = Utils.Instantiate( view ).GetComponent< TView >();
                UIRoot uiRoot = GameObject.FindObjectsByType< UIRoot >( FindObjectsSortMode.None )
                    .FirstOrDefault( item => item.InsranceID == _uiRootAggregator.InstanceID );

                if ( uiRoot != null )
                {
                    uiRoot.SetParent( _baseView.transform, baseDto.WindowLayer );
                }

                _baseView.OnStarted += ViewStartedHandler;

                void ViewStartedHandler() => OnInitialize( baseDto );
            }
            catch ( Exception e )
            {
                Debug.LogError( $"Error! Create View Failed {e.Message} {baseDto.WindowName}" );
            }
        }
    }
}