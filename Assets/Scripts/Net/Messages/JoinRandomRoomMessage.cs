using Common.Messages;

namespace StorageTest.Messages
{
    public readonly struct JoinRandomRoomMessage : IMessage
    {
        public readonly int MaxCountPlayer;
        public JoinRandomRoomMessage( int maxCountPlayer ) => MaxCountPlayer = maxCountPlayer;
    }
}