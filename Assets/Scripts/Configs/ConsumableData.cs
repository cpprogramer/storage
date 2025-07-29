using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Common.Configs
{
    [ Serializable ]
    public class ConsumableData
    {
        [ SerializeField ] private int _count;

        [ SerializeField ] private BaseConsumableItemConfig _data;

        public string Uid => _data.Uid;
        public int Count => _count;

        [ Conditional( "UNITY_EDITOR" ) ]
        public void Validate( Object context )
        {
            Assert.IsNotNull( _data, $"_data == null  {context.name}" );
            _data.Validate();
        }
    }
}