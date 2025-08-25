using Cysharp.Threading.Tasks;
using StorageTest.Model;
using UnityEngine;

namespace StorageTest.GamePlay
{
    public sealed class LevelViewDebug : MonoBehaviour, ILevelView
    {
        [ SerializeField ] private Camera _camera;
        [ SerializeField ] private Transform _parent;

        public async UniTask CreateView() {}

        public void SetCameraOverlay( Camera main, Rect cameraDataViewPort ) {}
    }
}