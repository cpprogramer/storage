using Cysharp.Threading.Tasks;
using System;

namespace MonopolySpace.Net
{
    public interface IMultiplayerBackend : IDisposable
    {
        IRoomManagement RoomManagement { get; }
        IConnectionManager ConnectionManager { get; }
        IMatchmakingManager MatchmakingManager { get; }
        ILobbyManager LobbyManager { get; }
        IMultiplayerMessaging MultiplayerMessaging { get; }
        IErrorInfoPhoton ErrorInfoPhoton { get; }
        bool IsConnectedAndready { get; }
        UniTask ConnectAsync();
        void Disconnect();
        UniTask JoinOrCreateRoom();
        UniTask JoinRoom( string roomUid );
        UniTask JoinLobbyAsync();
    }
}