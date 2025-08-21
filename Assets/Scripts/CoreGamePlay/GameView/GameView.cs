using Common;
using Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using StorageTest.Model;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using CameraData = Configs.CameraData;

namespace StorageTest.View
{
    public sealed class GameView : IGameView
    {
        public event Action OnLevelLoaded;

        public ILevelView LevelRoot { get; private set; }
        private readonly IGamePlayReadOnly _gamePlay;
        private readonly IResourcesProvider _resourcesProvider = new ResourcesProvider();
        private readonly IScenesManager _scenesManager;
        private readonly ILevelProvider _levelProvider;
        private readonly GameObject _view;
        private readonly int _instanceUid;
        private Camera _camera;
        private readonly CameraConfig _cameraConfig;

        public GameView(
            int instanceUid,
            IScenesManager scenesManager,
            IGamePlayReadOnly gamePlay
        )
        {
            _instanceUid = instanceUid;
            _scenesManager = scenesManager ?? throw new ArgumentNullException( nameof(scenesManager) );
            _gamePlay = gamePlay ?? throw new ArgumentNullException( nameof(gamePlay) );
            _view = new GameObject( "GameView" );
            _levelProvider = new DebugLevelProvider( _resourcesProvider );
            _cameraConfig = Resources.Load< CameraConfig >( "Configs/CameraConfig" );
        }

        public async UniTask Start() => SubscribeOrUnsubscribe( true );

        public async UniTask Initialize()
        {
            await LoadLevel();
            var tag = $"Camera_{_instanceUid}";
            _camera = Camera.allCameras.FirstOrDefault( cam => cam.CompareTag( tag ) );
            UniversalAdditionalCameraData data = _camera.GetUniversalAdditionalCameraData();
            data.renderType = CameraRenderType.Overlay;

            CameraData cameraData = _cameraConfig.GetDataByUID( _instanceUid );
            LevelRoot.SetCameraOverlay( _camera, cameraData.viewPort );
        }

        public void Dispose()
        {
            SubscribeOrUnsubscribe( false );
            UniversalAdditionalCameraData data = _camera.GetUniversalAdditionalCameraData();
            data.renderType = CameraRenderType.Base;
            _scenesManager.RemoveScene( _scenesManager.CurrentSceneName );
            Utils.Destroy( _view );
        }

        private async UniTask LoadLevel()
        {
            //LevelRoot = await _levelProvider.GetLevelAsync( _view.transform, config );
            OnLevelLoaded?.Invoke();
        }

        private void SubscribeOrUnsubscribe( bool isSubscribe )
        {
            if ( isSubscribe )
            {
            }
        }
    }
}