using Cysharp.Threading.Tasks;
using System;

namespace MonopolySpace.Net
{
    public interface IMultiplayerService : IDisposable
    {
        event Action< IRoomInfo[] > OnRoomListUpdate;
        bool InLobby { get; }
        bool IsConnectedAndReady { get; }
        IRoomInfo[] CachedRooms { get; }
        UniTask ConnectAsync();
        void Disconnect();
        UniTask JoinOrCreateRoom();
        UniTask JoinRoom(string roomName);
        UniTask JoinLobbyAsync();
    }
}