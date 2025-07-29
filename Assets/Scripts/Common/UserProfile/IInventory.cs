using System.Collections.Generic;

namespace Common
{
    public interface IInventory : IInventoryReadonly
    {
        //void CollectData( IDictionary< string, object > data );
        void Update( IDictionary< string, object > data );
        void UpdateConsumables( (string uid, int count, int last)[] data );
        void UpdateConsumables( string uid, int count, int last );
    }
}