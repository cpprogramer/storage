using Cysharp.Threading.Tasks;
using MonopolySpace.Lobby;
using System;

namespace MonopolySpace.Model
{
    public sealed class BotPlayer : BasePlayer
    {
        public BotPlayer( PlayerInfo info ) : base( info ) {}

        public override void MakeDecision( Action< PlayerDecision > onDecisionMade ) =>
            MakeDecisionInternal( onDecisionMade ).Forget();

        public override void MakeTurn() {}

        private async UniTaskVoid MakeDecisionInternal( Action< PlayerDecision > onDecisionMade )
        {
            await UniTask.Delay( TimeSpan.FromSeconds( 1 ) );
            onDecisionMade?.Invoke( new PlayerDecision() );
        }
    }
}