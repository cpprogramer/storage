using System.Collections.Generic;

namespace Common.Messages
{
    public sealed class UpdateConsumablesMessage : IMessage
    {
        public IEnumerable< (string Key, int ValueCurrent, int ValuePrev) > ConsumableData { get; }

        public UpdateConsumablesMessage(
            IEnumerable< (string Key, int ValueCurrent, int ValuePrev) > consumableData
        ) =>
            ConsumableData = consumableData;
    }
}