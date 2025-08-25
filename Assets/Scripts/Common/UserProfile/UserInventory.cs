using Common.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Common.Profile
{
    [ Serializable ]
    public sealed class UserInventory : IInventory
    {
        public IEnumerable< (string uid, int count) > Consumables =>
            _consumables.Select( elt => ( elt.Key, elt.Value ) );
        private readonly IMessageBroker _messageBroker;

        private Dictionary< string, int > _consumables;

        public UserInventory( IMessageBroker messageBroker )
        {
            _consumables = new Dictionary< string, int >();
            _messageBroker = messageBroker;
            _messageBroker.Receive< UpdateConsumableRecord >().Subscribe( UpdateConsumableRecordHandler );

            //var starterConfig = Resources.Load< StarterSetConsumablesConfig >( "configs/StarterSetConfig" );
            //(string Uid, int Count)[] starterConsumables = starterConfig.GetConsumableModels();
            //UpdateConsumables( starterConsumables );
        }

        public void UpdateConsumables( (string uid, int count, int last)[] data ) =>
            _messageBroker.Publish( new UpdateConsumableRecord( data ) );

        public void UpdateConsumables( string uid, int count, int countLast ) =>
            _messageBroker.Publish( new UpdateConsumableRecord( new[] { ( uid, count, countLast ) } ) );

        public void Update( IDictionary< string, object > data )
        {
            if ( data.TryGetValue( GetType().Name, out object result ) )
            {
                var json = result as string;
                if ( string.IsNullOrEmpty( json ) ) return;

                _consumables = JsonConvert.DeserializeObject< Dictionary< string, int > >( json );

                IEnumerable< (string Key, int Value, int) > consumableData =
                    _consumables.Select( kv => ( kv.Key, kv.Value, 0 ) );
                _messageBroker.Publish( new UpdateConsumablesMessage( consumableData ) );
            }
        }

        public int GetConsumableCount( string uid )
        {
            if ( _consumables.TryGetValue( uid, out int val ) ) return val;

            return 0;
        }

        public IEnumerable< (string Uid, int current) > GetConsumables()
        {
            foreach ( KeyValuePair< string, int > consumable in _consumables )
                yield return ( consumable.Key, consumable.Value );
        }

        private void UpdateConsumableRecordHandler( UpdateConsumableRecord record )
        {
            var array = new (string Key, int Count, int Prev)[record.ConsumableData.Count()];

            record.ConsumableData.ForEach( ( consumableData, i ) =>
            {
                _consumables.TryGetValue( consumableData.key, out int val );
                array[ i ] = ( consumableData.key, consumableData.valueCurrent, val );
                _consumables[ consumableData.key ] = consumableData.valueCurrent;
            } );
            _messageBroker.Publish( new UpdateConsumablesMessage( array ) );
        }
    }
}