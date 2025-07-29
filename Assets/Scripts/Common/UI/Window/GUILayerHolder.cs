using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    public class GUILayerHolder : MonoBehaviour, IGUILayerHolder
    {
      
        private readonly Dictionary< WindowLayer, Transform > _layers = new();

        public Transform GetLayer( WindowLayer layer )
        {
            if ( _layers.ContainsKey( layer ) ) return _layers[ layer ];

            return null;
        }

        private void Awake()
        {
            UILayer[] arr = transform.GetComponentsInChildren< UILayer >( true );

            foreach ( UILayer layer in arr ) _layers.Add( layer.layer, layer.transform );
        }
    }
}