using Common.Messages;

namespace MonopolySpace.Messages
{
    public sealed class JoinRandomRoomMessage : IMessage
    {
        public int MaxCountPlayer { get; }
        public JoinRandomRoomMessage( int maxCountPlayer ) => MaxCountPlayer = maxCountPlayer;
    }
}