using System.Collections.Generic;

namespace Common.Messages
{
    public sealed class UpdateConsumableRecord : BaseRecordMessage
    {
        public IEnumerable< (string key, int valueCurrent, int valueLast) > ConsumableData { get; }

        public UpdateConsumableRecord( IEnumerable< (string key, int value, int valueLast) > consumableData ) =>
            ConsumableData = consumableData;
    }
}