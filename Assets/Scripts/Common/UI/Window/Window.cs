using Common.UI.Model;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UI;
using UnityEngine;
using CWindow = Common.UI.IWindow< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public abstract class Window< TView > : CWindow
        where TView : MonoBehaviour, IBaseView
    {
        public event Action< CWindow, WindowResult > OnClosingFinished;
        public event Action< CWindow > OnClosingStarted;

        public Type TypeWindow => GetType();
        public string WindowName { get; private set; }

        protected TView _baseView;

        protected readonly IUiRootAggregator _uiRootAggregator;
        private readonly IResourcesProvider _resourcesProvider;

        protected Window( IUiRootAggregator uiRootAggregator )
        {
            _uiRootAggregator = uiRootAggregator ?? throw new ArgumentNullException( nameof(uiRootAggregator) );
            _resourcesProvider = new ResourcesProvider();
        }

        protected abstract void OnInitialize( BaseWindowModel model );

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

        public void Initialize( BaseWindowModel baseModel )
        {
            WindowName = baseModel.WindowName;
            InitializeAsync( baseModel ).Forget();
        }

        private async UniTaskVoid InitializeAsync( BaseWindowModel baseModel )
        {
            try
            {
                var view = await _resourcesProvider.LoadResourceAsync< GameObject >( baseModel.WindowName );
                _baseView = Utils.Instantiate( view ).GetComponent< TView >();
                UIRoot uiRoot = GameObject.FindObjectsByType< UIRoot >( FindObjectsSortMode.None )
                    .FirstOrDefault( item => item.InsranceID == _uiRootAggregator.InstanceID );

                if ( uiRoot != null )
                {
                    uiRoot.SetParent( _baseView.transform, baseModel.WindowLayer );
                }

                _baseView.OnStarted += ViewStartedHandler;

                void ViewStartedHandler() => OnInitialize( baseModel );
            }
            catch ( Exception e )
            {
                Debug.LogError( $"Error! Create View Failed {e.Message}" );
            }
        }
    }
}