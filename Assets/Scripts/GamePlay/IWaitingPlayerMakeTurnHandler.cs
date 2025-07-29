using System;
using System.Threading;

namespace MonopolySpace.Model
{
    public interface IWaitingPlayerMakeTurnHandler
    {
        event Action< IPlayerReadOnly > OnNeedMakeTurn;
        event Action< float > OnTimerTurnStarted;
        event Action OnTimerTurnStopped;
        event Action< IPlayerReadOnly > OnTurnNotBeenMade;
        void MakeTurn( IPlayerReadOnly playerReadOnly, CancellationTokenSource cancellationTokenSource );
    }
}