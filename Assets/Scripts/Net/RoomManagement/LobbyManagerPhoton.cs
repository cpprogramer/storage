using Cysharp.Threading.Tasks;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonopolySpace.Net
{
    public sealed class LobbyManagerPhoton : ILobbyManager, ILobbyCallbacks
    {
        public event Action OnJoinedLobbyEvent;
        public event Action OnLeftLobbyEvent;
        public event Action OnLobbyStatisticsUpdateEvent;
        public event Action< IRoomInfo[] > OnRoomListUpdateEvent;

        public bool InLobby => _realtimeClient.InLobby;

        public IReadOnlyList< IRoomInfo > CachedRooms => _cachedRooms;
        private readonly RealtimeClient _realtimeClient;

        private readonly List< IRoomInfo > _cachedRooms = new();

        public LobbyManagerPhoton( RealtimeClient realtimeClient ) =>
            _realtimeClient = realtimeClient ?? throw new ArgumentNullException( nameof(realtimeClient) );

        void ILobbyCallbacks.OnJoinedLobby() => OnJoinedLobbyEvent?.Invoke();

        void ILobbyCallbacks.OnLeftLobby() => OnLeftLobbyEvent?.Invoke();

        void ILobbyCallbacks.OnLobbyStatisticsUpdate( List< TypedLobbyInfo > lobbyStatistics )
        {
            Debug.LogError( "[+] OnLobbyStatisticsUpdate" );
            OnLobbyStatisticsUpdateEvent?.Invoke();
        }

        void ILobbyCallbacks.OnRoomListUpdate( List< RoomInfo > roomList )
        {
            Debug.LogError( $"[+] OnRoomListUpdate count:{roomList.Count}" );

            _cachedRooms.Clear();

            IRoomInfo[] rooms = roomList /*.Where( item => item.IsVisible && !item.RemovedFromList && item.IsOpen )*/
                .Select( item => new PhotonRoomInfo( item.Name ) as IRoomInfo ).ToArray();

            _cachedRooms.AddRange( rooms );

            OnRoomListUpdateEvent?.Invoke( rooms );
        }

        public async UniTask JoinLobbyAsync()
        {
            if ( InLobby )
                return;

            await _realtimeClient.JoinLobbyAsync();
        }

        public async UniTask LeaveLobbyAsync()
        {
            if ( !InLobby )
                return;

            await _realtimeClient.LeaveLobbyAsync();
        }
    }
}