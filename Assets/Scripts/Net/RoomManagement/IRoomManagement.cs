using System;

namespace MonopolySpace.Net
{
    public interface IRoomManagement
    {
        event Action OnMasterClientSwitchedEvent;
        event Action OnPlayerEnteredRoomEvent;
        event Action OnPlayerLeftRoomEvent;
        event Action OnPlayerPropertiesUpdateEvent;
        event Action OnRoomPropertiesUpdateEvent;
    }
}