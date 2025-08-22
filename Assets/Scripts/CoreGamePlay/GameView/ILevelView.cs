using Common;
using UnityEngine;

namespace StorageTest.Model
{
    public interface ILevelView
    {
        void SetCameraOverlay(Camera main, Rect cameraDataViewPort);
    }
}