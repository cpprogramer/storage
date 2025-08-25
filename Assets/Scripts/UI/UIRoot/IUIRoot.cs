using Common;
using UnityEngine;

namespace UI.UIRoot
{
    public interface IUIRoot
    {
        int InsranceID { get; }
        void Setup( int instanceUid, Rect cameraDataViewPort, bool isDebugMode = false );

        void SetParent( Transform child, WindowLayer layerParent );
    }
}