using System.Collections.Generic;

namespace Common
{
    public interface IInventoryReadonly
    {
        IEnumerable< (string uid, int count) > Consumables { get; }
        int GetConsumableCount( string uid );
    }
}