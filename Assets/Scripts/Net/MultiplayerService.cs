using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace StorageTest.Net
{
    public sealed class MultiplayerService : IMultiplayerService
    {
        public event Action< IRoomInfo[] > OnRoomListUpdate;

        private readonly IMultiplayerBackend _multiplayerBackend;
        private readonly IMessageBroker _messageBroker;

        bool IMultiplayerService.InLobby => _multiplayerBackend.LobbyManager.InLobby;
        bool IMultiplayerService.IsConnectedAndReady => _multiplayerBackend.IsConnectedAndready;

        IRoomInfo[] IMultiplayerService.CachedRooms => _multiplayerBackend.LobbyManager.CachedRooms.ToArray();

        public MultiplayerService( IMultiplayerBackend multiplayerBackend, IMessageBroker messageBroker )
        {
            _multiplayerBackend = multiplayerBackend;
            _messageBroker = messageBroker;
            SubscribeOrUnsubscribe( true );
        }

        public void Dispose() => SubscribeOrUnsubscribe( false );

        async UniTask IMultiplayerService.JoinOrCreateRoom() => await _multiplayerBackend.JoinOrCreateRoom();

        async UniTask IMultiplayerService.JoinRoom( string roomUid ) => await _multiplayerBackend.JoinRoom( roomUid );

        async UniTask IMultiplayerService.JoinLobbyAsync() => await _multiplayerBackend.JoinLobbyAsync();

        void IMultiplayerService.Disconnect() => _multiplayerBackend.Disconnect();

        async UniTask IMultiplayerService.ConnectAsync() => await _multiplayerBackend.ConnectAsync();

        private void ConnectedToMasterHandler() =>
            _multiplayerBackend.ConnectionManager.OnConnectedToMasterEvent -= ConnectedToMasterHandler;

        private void DisconnectedEventHandler( DisconnectCause cause ) => Debug.LogError( $"[+] Disconnect {cause}" );

        private void RoomListUpdateEventHandler( IRoomInfo[] roomInfos ) => OnRoomListUpdate?.Invoke( roomInfos );

        private void SubscribeOrUnsubscribe( bool isSubscribe )
        {
            if ( isSubscribe )
            {
                _multiplayerBackend.ConnectionManager.OnDisconnectedEvent += DisconnectedEventHandler;
                _multiplayerBackend.ConnectionManager.OnConnectedToMasterEvent += ConnectedToMasterHandler;
                _multiplayerBackend.LobbyManager.OnRoomListUpdateEvent += RoomListUpdateEventHandler;
            }
            else
            {
                _multiplayerBackend.ConnectionManager.OnDisconnectedEvent -= DisconnectedEventHandler;
                _multiplayerBackend.ConnectionManager.OnConnectedToMasterEvent -= ConnectedToMasterHandler;
                _multiplayerBackend.LobbyManager.OnRoomListUpdateEvent -= RoomListUpdateEventHandler;
            }
        }
    }
}