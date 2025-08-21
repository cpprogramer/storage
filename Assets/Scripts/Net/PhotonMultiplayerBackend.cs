using Common;
using Cysharp.Threading.Tasks;
using Photon.Client;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace StorageTest.Net
{
    public sealed class PhotonMultiplayerBackend : IMultiplayerBackend
    {
        public IRoomManagement RoomManagement { get; }
        public IConnectionManager ConnectionManager { get; }
        public IMatchmakingManager MatchmakingManager { get; }
        public ILobbyManager LobbyManager { get; }
        public IMultiplayerMessaging MultiplayerMessaging { get; }
        public IErrorInfoPhoton ErrorInfoPhoton { get; }

        private readonly ITickable _tickable;
        private readonly RealtimeClient _realtimeClient;
        private readonly List< object > _callbacks = new();

        bool IMultiplayerBackend.IsConnectedAndready => _realtimeClient.IsConnectedAndReady;

        public PhotonMultiplayerBackend(
            ITickable tickable,
            IMessageBroker messageBroker,
            VersionNumberProvider versionNumberProvider
        )
        {
            _realtimeClient = new RealtimeClient( ConnectionProtocol.Tcp );
            _tickable = tickable ?? throw new ArgumentNullException( nameof(tickable) );
            RoomManagement = new RoomManagementPhoton( _realtimeClient );
            _callbacks.Add( RoomManagement );

            ConnectionManager = new PhotonConnectionManager( _realtimeClient );
            _callbacks.Add( ConnectionManager );

            MatchmakingManager = new MatchmakingManagerPhoton( _realtimeClient );
            _callbacks.Add( MatchmakingManager );

            LobbyManager = new LobbyManagerPhoton( _realtimeClient );
            _callbacks.Add( LobbyManager );

            MultiplayerMessaging = new MultiplayerMessagingPhoton( _realtimeClient );
            _callbacks.Add( MultiplayerMessaging );

            ErrorInfoPhoton = new ErrorInfoPhoton( _realtimeClient );
            _callbacks.Add( ErrorInfoPhoton );
        }

        public void Dispose() => Disconnect();

        private void TickHandler() => _realtimeClient.Service();

        private void InitializeWithCallbacks() => _callbacks.ForEach( item => { _realtimeClient.AddCallbackTarget( item ); } );

        private void StateChangeHandler( ClientState previousState, ClientState currentState ) =>
            Debug.LogError(
                $"[+] pewvState: {previousState}  currstate:{currentState} inLobby:{_realtimeClient.InLobby}" );

        async UniTask IMultiplayerBackend.JoinOrCreateRoom() => await _realtimeClient.JoinRandomOrCreateRoomAsync();
        
        async UniTask IMultiplayerBackend.JoinRoom(string roomUid) => await _realtimeClient.JoinRoomAsync( new EnterRoomArgs() { RoomName = roomUid } );

        async UniTask IMultiplayerBackend.JoinLobbyAsync() => await _realtimeClient.JoinLobbyAsync();

        private void RemoveCallbacks()
        {
            _callbacks.ForEach( item => { _realtimeClient.RemoveCallbackTarget( item ); } );

            _callbacks.Clear();
        }

        async UniTask IMultiplayerBackend.ConnectAsync()
        {
            _realtimeClient.StateChanged += StateChangeHandler;

            await _realtimeClient.ConnectUsingSettingsAsync( new AppSettings
            {
                AppIdRealtime = "3c7ebdbc-139a-47c8-bfa8-eb7b93e22792", FixedRegion = "eu"
            } );

            Debug.LogError( $"[+] IsConnectedAndReady:{_realtimeClient.IsConnectedAndReady}" );
            if ( _realtimeClient.IsConnectedAndReady )
            {
                InitializeWithCallbacks();
                _tickable.OnTick += TickHandler;
            }
        }

        public void Disconnect()
        {
            RemoveCallbacks();
            _realtimeClient.StateChanged -= StateChangeHandler;
            _tickable.OnTick -= TickHandler;
            _realtimeClient.Disconnect();
        }
    }
}