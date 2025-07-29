using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace MonopolySpace.Net
{
    public static class PhotonExtension
    {
        public static DisconnectCause Convert( this Photon.Realtime.DisconnectCause photonCause ) =>
            (DisconnectCause)photonCause;
    }

    public sealed class PhotonConnectionManager : IConnectionManager, IConnectionCallbacks
    {
        public event Action OnConnectedToMasterEvent;
        public event Action OnCustomAuthenticationFailedEvent;
        public event Action OnCustomAuthenticationResponseEvent;
        public event Action< DisconnectCause > OnDisconnectedEvent;
        public event Action OnRegionListReceivedEvent;
        private readonly RealtimeClient _realtimeClient;

        public PhotonConnectionManager( RealtimeClient realtimeClient ) =>
            _realtimeClient = realtimeClient ?? throw new ArgumentNullException( nameof(realtimeClient) );

        void IConnectionCallbacks.OnConnectedToMaster() => OnConnectedToMasterEvent?.Invoke();

        void IConnectionCallbacks.OnDisconnected( Photon.Realtime.DisconnectCause cause ) =>
            OnDisconnectedEvent?.Invoke( cause.Convert() );

        void IConnectionCallbacks.OnRegionListReceived( RegionHandler regionHandler ) =>
            OnRegionListReceivedEvent?.Invoke();

        void IConnectionCallbacks.OnCustomAuthenticationResponse( Dictionary< string, object > data ) =>
            OnCustomAuthenticationResponseEvent?.Invoke();

        void IConnectionCallbacks.OnCustomAuthenticationFailed( string debugMessage ) =>
            OnCustomAuthenticationFailedEvent?.Invoke();

        [ Obsolete ] void IConnectionCallbacks.OnConnected() {} //obsolete
    }
}