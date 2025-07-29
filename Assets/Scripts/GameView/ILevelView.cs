using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MonopolySpace.Model
{
    public interface ILevelView
    {
        UniTask CreateView(  );
        void SetCameraOverlay( Camera main, Rect cameraDataViewPort );
    }
}