using System;

namespace StorageTest.Net
{
    public interface IMultiplayerMessaging
    {
        event Action OnMessage;
    }
}