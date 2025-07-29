using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonopolySpace.Net
{
    public sealed class MatchmakingManagerPhoton : IMatchmakingManager, IMatchmakingCallbacks
    {
        public event Action OnCreatedRoomEvent;
        public event Action OnCreateRoomFailedEvent;
        public event Action OnFriendListUpdateEvent;
        public event Action OnJoinedRoomEvent;
        public event Action OnJoinRandomFailedEvent;
        public event Action OnJoinRoomFailedEvent;
        public event Action OnLeftRoomEvent;

        private readonly RealtimeClient _realtimeClient;

        public MatchmakingManagerPhoton( RealtimeClient realtimeClient ) =>
            _realtimeClient = realtimeClient ?? throw new ArgumentNullException( nameof(realtimeClient) );

        void IMatchmakingCallbacks.OnFriendListUpdate( List< FriendInfo > friendList ) =>
            OnFriendListUpdateEvent?.Invoke();

        void IMatchmakingCallbacks.OnJoinRoomFailed( short returnCode, string message ) =>
            OnJoinRoomFailedEvent?.Invoke();

        void IMatchmakingCallbacks.OnJoinRandomFailed( short returnCode, string message ) =>
            OnJoinRandomFailedEvent?.Invoke();

        void IMatchmakingCallbacks.OnLeftRoom() => OnLeftRoomEvent?.Invoke();

        void IMatchmakingCallbacks.OnCreatedRoom()
        {
            Debug.LogError( "[+] OnCreatedRoom" );
            OnCreatedRoomEvent?.Invoke();
        }

        void IMatchmakingCallbacks.OnCreateRoomFailed( short returnCode, string message )
        {
            Debug.LogError( "[+] OnCreateRoomFailed" );
            OnCreateRoomFailedEvent?.Invoke();
        }

        void IMatchmakingCallbacks.OnJoinedRoom()
        {
            Debug.LogError( "[+] OnJoinedRoom" );
            OnJoinedRoomEvent?.Invoke();
        }
    }
}