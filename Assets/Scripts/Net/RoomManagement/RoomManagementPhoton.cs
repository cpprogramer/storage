using Photon.Client;
using Photon.Realtime;
using System;
using UnityEngine;

namespace StorageTest.Net
{
    public sealed class RoomManagementPhoton : IRoomManagement, IInRoomCallbacks
    {
        public event Action OnMasterClientSwitchedEvent;
        public event Action OnPlayerEnteredRoomEvent;
        public event Action OnPlayerLeftRoomEvent;
        public event Action OnPlayerPropertiesUpdateEvent;
        public event Action OnRoomPropertiesUpdateEvent;

        private RealtimeClient _realtimeClient;

        public RoomManagementPhoton( RealtimeClient realtimeClient ) =>
            _realtimeClient = realtimeClient ?? throw new ArgumentNullException( nameof(realtimeClient) );

        void IInRoomCallbacks.OnPlayerLeftRoom( Player otherPlayer ) => OnPlayerLeftRoomEvent?.Invoke();

        void IInRoomCallbacks.OnPlayerPropertiesUpdate( Player targetPlayer, PhotonHashtable changedProps ) =>
            OnPlayerPropertiesUpdateEvent?.Invoke();

        void IInRoomCallbacks.OnMasterClientSwitched( Player newMasterClient ) => OnMasterClientSwitchedEvent?.Invoke();

        void IInRoomCallbacks.OnPlayerEnteredRoom( Player newPlayer )
        {
            Debug.LogError( "[+] ENTER" );
            OnPlayerEnteredRoomEvent?.Invoke();
        }

        void IInRoomCallbacks.OnRoomPropertiesUpdate( PhotonHashtable propertiesThatChanged )
        {
            Debug.LogError( "[+] OnRoomPropertiesUpdate" );
            OnRoomPropertiesUpdateEvent?.Invoke();
        }
    }
}