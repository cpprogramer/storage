using System;

namespace StorageTest.Net
{
    public interface IMatchmakingManager
    {
        event Action OnCreatedRoomEvent;
        event Action OnCreateRoomFailedEvent;
        event Action OnFriendListUpdateEvent;
        event Action OnJoinedRoomEvent;
        event Action OnJoinRandomFailedEvent;
        event Action OnJoinRoomFailedEvent;
        event Action OnLeftRoomEvent;
    }
}