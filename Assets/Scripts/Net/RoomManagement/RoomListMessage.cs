using Common.Messages;
using StorageTest.Net;

namespace StorageTest.Messages
{
    public readonly struct RoomListMessage : IMessage
    {
        public IRoom[] Rooms { get; }

        public RoomListMessage( IRoom[] rooms ) => Rooms = rooms;
    }
}