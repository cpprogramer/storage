using Photon.Realtime;
using System;

namespace MonopolySpace.Net
{
    public sealed class ErrorInfoPhoton : IErrorInfoPhoton, IErrorInfoCallback
    {
        public event Action OnErrorEvent;
        private readonly RealtimeClient _realtimeClient;

        public ErrorInfoPhoton( RealtimeClient realtimeClient ) =>
            _realtimeClient = realtimeClient ?? throw new ArgumentNullException( nameof(realtimeClient) );

        void IErrorInfoCallback.OnErrorInfo( ErrorInfo errorInfo ) => OnErrorEvent?.Invoke();
    }
}