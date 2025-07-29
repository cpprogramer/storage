using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Configs
{
    public class StarterSetConsumablesConfig : BaseConfig
    {
        [ SerializeField ] private ConsumableData[] _consumableItems;

        public (string Uid, int Count)[] GetConsumableModels() =>
            _consumableItems.Select( elt => ( elt.Uid, elt.Count ) ).ToArray();

        public override void Validate()
        {
            Assert.IsFalse( _consumableItems.IsNullOrEmpty() );
            _consumableItems.ForEach( elt => elt.Validate( this ) );
        }
    }
}