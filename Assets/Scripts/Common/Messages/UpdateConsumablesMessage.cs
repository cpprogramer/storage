using System.Collections.Generic;

namespace Common.Messages
{
    public readonly struct UpdateConsumablesMessage : IMessage
    {
        public readonly IEnumerable< (string Key, int ValueCurrent, int ValuePrev) > ConsumableData;

        public UpdateConsumablesMessage(
            IEnumerable< (string Key, int ValueCurrent, int ValuePrev) > consumableData
        ) =>
            ConsumableData = consumableData;
    }
}