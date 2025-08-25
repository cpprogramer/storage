using Common;
using Common.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIRoot
{
    public sealed class UIRoot : MonoBehaviour, IUIRoot
    {
        [ SerializeField ] private Canvas _main;
        [ SerializeField ] private TextMeshProUGUI _text;
        [ SerializeField ] private Image _imageDebugFrame;
        public int InsranceID { get; private set; }

        private IGUILayerHolder _layers;

        public void Setup( int instanceUid, Rect cameraDataViewPort, bool isDebugMode = false )
        {
            InsranceID = instanceUid;
            _text.text = instanceUid.ToString();
            var camera = GetComponentInChildren< Camera >();
            camera.tag = $"Camera_{instanceUid}";
            camera.rect = cameraDataViewPort;
            _imageDebugFrame.gameObject.SetActive( isDebugMode );
        }

        public void SetParent( Transform child, WindowLayer layerParent )
        {
            try
            {
                if ( _layers == null ) InitializeLayers();
                Transform parent = _layers.GetLayer( layerParent );
                child.SetParent( parent, false );
            }
            catch ( Exception e )
            {
                Debug.LogError( $"[+] Error: {e.Message}" );
                throw;
            }
        }

        private void Awake() => InitializeLayers();

        private void InitializeLayers() => _layers = gameObject.GetComponentInChildren< IGUILayerHolder >();
    }
}