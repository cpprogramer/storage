using System;
using StorageTest.Lobby;

namespace StorageTest.Model
{
    public abstract class BasePlayer : IPlayer
    {
        public PlayerInfo PlayerInfo { get; }

        protected BasePlayer( PlayerInfo playerInfo ) => PlayerInfo = playerInfo;
        
    }
}