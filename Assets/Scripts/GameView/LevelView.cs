using Common;
using Cysharp.Threading.Tasks;
using MonopolySpace.Model;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MonopolySpace.GamePlay
{
    public sealed class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _parent;

        private IResourcesCache _resourcesCache;
        private Vector2 _sideMaxMin;

        private void OnDestroy()
        {
            _resourcesCache.Dispose();
        }


        public async UniTask CreateView()
        {
        }

        public void SetCameraOverlay(Camera main, Rect cameraDataViewPort)
        {
            var data = _camera.GetUniversalAdditionalCameraData();
            data.cameraStack.Add(main);
            _camera.rect = cameraDataViewPort;
        }

        private float GetDeltaSize()
        {
            return (_sideMaxMin.x - _sideMaxMin.y) * 0.5f;
        }

        public void Setup(IResourcesProvider resourcesProvider, Transform parent)
        {
            _resourcesCache = new ResourcesCache(resourcesProvider);
            transform.SetParent(parent, false);
        }
    }
}