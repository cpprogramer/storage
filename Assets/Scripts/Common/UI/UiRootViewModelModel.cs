using Assets.Scripts.Timeline;
using Configs;
using Cysharp.Threading.Tasks;
using MonopolySpace;
using System;
using UniRx;
using UnityEngine;
using CWindow = Common.UI.IWindow< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public sealed class UiRootViewModel : IUiRootViewModel
    {
        public event Action OnInitialized;

        private readonly IUiManager< Type, WindowResult > _uiManager;
        private readonly int _instanceUid;
        private readonly IResourcesProvider _resourcesProvider = new ResourcesProvider();
        private readonly IMessageBroker _messageBroker;
        private readonly IParentHolder _parentHolder;
        
        private IUIRoot _uiRoot;
        private ITimeline _timeline;
        private readonly bool _isDebugMode;
        
        public UiRootViewModel(
            int instanceUid,
            bool isDebugMode,
            IParentHolder parentHolder,
            IMessageBroker messageBroker
        )
        {
            _instanceUid = instanceUid;
            _isDebugMode = isDebugMode;
            _parentHolder = parentHolder ?? throw new ArgumentNullException( nameof(parentHolder) );
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            _uiManager = new UiManager( _messageBroker );
        }
      
        public void RegisterWindow( Type type, Func<CWindow> func )
        {
            _uiManager.RegisterWindow( type, func );
        }

        void IUiRootViewModel.Initialize()
        {
            InitializeAsync().Forget();
        }
        
        private async UniTaskVoid InitializeAsync(  )
        {
            var rootGameObj = await _resourcesProvider.LoadResourceAsync< GameObject >( "UIRoot" );
            GameObject inst = Utils.Instantiate( rootGameObj );

            _uiRoot = inst.GetComponent< IUIRoot >();
            var cameraConfig = Resources.Load< CameraConfig >( "Configs/CameraConfig" );
            CameraData cameraData = cameraConfig.GetDataByUID( _instanceUid );
            _uiRoot.Setup( _instanceUid, cameraData.viewPort, _isDebugMode );
            inst.name = $"UIRoot_{_instanceUid}";
            _parentHolder.Attach( inst.transform );
            _timeline = new Timeline(  );

            OnInitialized?.Invoke();
        }

        void IUiRootViewModel.ReleaseView( string name ) => _resourcesProvider.Release( name );
    }
}