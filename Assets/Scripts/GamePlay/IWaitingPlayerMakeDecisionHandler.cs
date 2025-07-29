using System;
using System.Threading;

namespace MonopolySpace.Model
{
    public interface IWaitingPlayerMakeDecisionHandler
    {
        event Action< IPlayerReadOnly > OnDecisionNotBeenMade;
        event Action< IPlayerReadOnly > OnNeedMakeDecision;

        event Action< float > OnTimerDecisionStarted;
        event Action OnTimerDecisionStopped;
        void MakeDecision( IPlayerReadOnly playerReadOnly, CancellationTokenSource cancellationTokenSource );
    }
}