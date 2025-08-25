using Configs;
using Cysharp.Threading.Tasks;
using StorageTest;
using System;
using UI.UIRoot;
using UnityEngine;

namespace Common.UI
{
    public sealed class UiRootViewModel : IUiRootViewModel
    {
        private readonly int _instanceUid;
        private readonly IResourcesProvider _resourcesProvider = new ResourcesProvider();
        private readonly IParentHolder _parentHolder;

        private IUIRoot _uiRoot;
        private readonly bool _isDebugMode;

        public UiRootViewModel( int instanceUid, bool isDebugMode, IParentHolder parentHolder )
        {
            _instanceUid = instanceUid;
            _isDebugMode = isDebugMode;
            _parentHolder = parentHolder ?? throw new ArgumentNullException( nameof(parentHolder) );
        }

        async UniTask IUiRootViewModel.InitializeAsync()
        {
            var rootGameObj = await _resourcesProvider.LoadResourceAsync< GameObject >( "UIRoot" );
            GameObject inst = Utils.Instantiate( rootGameObj );

            _uiRoot = inst.GetComponent< IUIRoot >();
            var cameraConfig = Resources.Load< CameraConfig >( "Configs/CameraConfig" );
            CameraData cameraData = cameraConfig.GetDataByUID( _instanceUid );
            _uiRoot.Setup( _instanceUid, cameraData.viewPort, _isDebugMode );
            inst.name = $"UIRoot_{_instanceUid}";
            _parentHolder.Attach( inst.transform );
        }
    }
}