using Common;
using Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Configs_CameraData = Configs.CameraData;

namespace StorageTest.Model
{
    public sealed class HangarView
    {
        private readonly Hangar.Models.Hangar _hangar;
        private readonly IScenesManager _scenesManager;
        private ILevelView _levelView;
        private Camera _camera;
        private readonly CameraConfig _cameraConfig;
        private readonly int _instanceId;

        public HangarView( Hangar.Models.Hangar hangar, IScenesManager scenesManager, int instanceId )
        {
            _hangar = hangar ?? throw new ArgumentNullException( nameof(hangar) );
            _scenesManager = scenesManager ?? throw new ArgumentNullException( nameof(scenesManager) );
            _instanceId = instanceId;

            _hangar.OnHangarStarted += HangarStartedHandler;
            _cameraConfig = Resources.Load< CameraConfig >( "Configs/CameraConfig" );
        }

        public async UniTask Initialize()
        {
            Scene scene = await _scenesManager.AddSceneAsync( "TestGamePlay" );
            GameObject rootSceneObject = scene.GetRootGameObjects().First();
            _levelView = rootSceneObject.GetComponent< ILevelView >();

            var tag = $"Camera_{_instanceId}";
            _camera = Camera.allCameras.FirstOrDefault( cam => cam.CompareTag( tag ) );
            UniversalAdditionalCameraData data = _camera.GetUniversalAdditionalCameraData();
            data.renderType = CameraRenderType.Overlay;

            Configs_CameraData cameraData = _cameraConfig.GetDataByUID( _instanceId );
            _levelView.SetCameraOverlay( _camera, cameraData.viewPort );
        }

        private void HangarStartedHandler() => _hangar.OnHangarStarted -= HangarStartedHandler;
    }
}