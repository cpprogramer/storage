namespace MonopolySpace.Net
{
    public readonly struct MessageData
    {
        public readonly string PlayerId;
        public readonly object Data;
        public readonly MessageCode Code;

        public MessageData( string playerId, MessageCode code, object data )
        {
            PlayerId = playerId;
            Code = code;
            Data = data;
        }
    }
}