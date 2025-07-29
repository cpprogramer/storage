using Cysharp.Threading.Tasks;
using MonopolySpace.Model;
using UnityEngine;

namespace MonopolySpace.GamePlay
{
    public sealed class LevelViewDebug : MonoBehaviour, ILevelView
    {
        [ SerializeField ] private Camera _camera;
        [ SerializeField ] private Transform _parent;
        public async UniTask CreateView(  )
        {
        }

        public void SetCameraOverlay( Camera main, Rect cameraDataViewPort ) {}
    }
}