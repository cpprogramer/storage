using System;

namespace MonopolySpace.Model
{
    public interface IPlayersTurnSequenceHandler
    {
        event Action< int > OnNeedMakeTurn;
        void Start();
        void SwitchToNextPlayer();
    }
}