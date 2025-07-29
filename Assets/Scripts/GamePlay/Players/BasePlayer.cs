using MonopolySpace.Lobby;
using System;

namespace MonopolySpace.Model
{
    public abstract class BasePlayer : IPlayer
    {
        public PlayerInfo PlayerInfo { get; }

        protected BasePlayer( PlayerInfo playerInfo ) => PlayerInfo = playerInfo;

        public abstract void MakeDecision( Action< PlayerDecision > onDecisionMade );

        public abstract void MakeTurn();
    }
}