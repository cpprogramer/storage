using System;

namespace MonopolySpace.Net
{
    public interface IMultiplayerMessaging
    {
        event Action OnMessage;
    }
}