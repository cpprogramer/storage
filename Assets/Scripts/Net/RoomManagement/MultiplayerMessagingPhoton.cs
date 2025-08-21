using Photon.Client;
using Photon.Realtime;
using System;

namespace StorageTest.Net
{
    public sealed class MultiplayerMessagingPhoton : IMultiplayerMessaging, IOnEventCallback
    {
        public event Action OnMessage;
        private readonly RealtimeClient _realtimeClient;

        public MultiplayerMessagingPhoton( RealtimeClient realtimeClient ) =>
            _realtimeClient = realtimeClient ?? throw new ArgumentNullException( nameof(realtimeClient) );

        void IOnEventCallback.OnEvent( EventData photonEvent ) => OnMessage?.Invoke();
    }
}