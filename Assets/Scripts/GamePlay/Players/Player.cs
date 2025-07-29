using MonopolySpace.Lobby;
using System;

namespace MonopolySpace.Model
{
    public sealed class Player : BasePlayer
    {
        private readonly IGamePlayManager _gamePlayManager;

        public Player( PlayerInfo info, IGamePlayManager gamePlayManager ) : base( info ) =>
            _gamePlayManager = gamePlayManager;

        public override void MakeDecision( Action< PlayerDecision > onDecisionMade ) {}

        public override void MakeTurn() {}
    }
}