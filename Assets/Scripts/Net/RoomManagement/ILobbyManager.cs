using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace StorageTest.Net
{
    public interface ILobbyManager
    {
        event Action OnJoinedLobbyEvent;
        event Action OnLeftLobbyEvent;
        event Action OnLobbyStatisticsUpdateEvent;
        event Action< IRoomInfo[] > OnRoomListUpdateEvent;

        IReadOnlyList< IRoomInfo > CachedRooms { get; }
        bool InLobby { get; }

        UniTask LeaveLobbyAsync();
        UniTask JoinLobbyAsync();
    }
}