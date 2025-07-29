using Common.Messages;
using MonopolySpace.Net;

namespace MonopolySpace.Messages
{
    public sealed class RoomListMessage : IMessage
    {
        public IRoom[] Rooms { get; }

        public RoomListMessage( IRoom[] rooms ) => Rooms = rooms;
    }
}