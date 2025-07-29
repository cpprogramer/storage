using System;

namespace MonopolySpace.Model
{
    public interface IPlayer : IPlayerReadOnly
    {
        void MakeTurn();
        void MakeDecision( Action< PlayerDecision > onDecisionMade );
    }
}