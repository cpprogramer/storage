using Common.Configs;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Configs
{
    public sealed class ConsumablesConfig : ScriptableObject, IConsumablesConfig
    {
        [ SerializeField ] private BaseConsumableItemConfig _gold;
        //[ SerializeField ] private BaseConsumableItemConfig _silver;
        public string GoldUid => _gold.Uid;

        [ Conditional( "UNITY_EDITOR" ) ]
        private void OnValidate()
        {
            Assert.IsNotNull( _gold, "_gold == null" );
            //Assert.IsNotNull( _silver, "_silver == null" );
        }
    }
}