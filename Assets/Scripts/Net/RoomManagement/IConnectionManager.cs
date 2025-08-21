using System;

namespace StorageTest.Net
{
    public interface IConnectionManager
    {
        event Action OnConnectedToMasterEvent;
        event Action OnCustomAuthenticationFailedEvent;
        event Action OnCustomAuthenticationResponseEvent;
        event Action< DisconnectCause > OnDisconnectedEvent;
        event Action OnRegionListReceivedEvent;
    }
}