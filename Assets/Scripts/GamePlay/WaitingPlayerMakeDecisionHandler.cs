using Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace MonopolySpace.Model
{
    public sealed class WaitingPlayerMakeDecisionHandler : IWaitingPlayerMakeDecisionHandler
    {
        public event Action< IPlayerReadOnly > OnDecisionNotBeenMade;
        public event Action< IPlayerReadOnly > OnNeedMakeDecision;

        public event Action< float > OnTimerDecisionStarted;
        public event Action OnTimerDecisionStopped;

        private readonly IGamePlayConfig _gamePlayConfig;

        private CancellationTokenSource _cancellationTokenDecision;

        private bool _isDecisionMade;

        public WaitingPlayerMakeDecisionHandler( IGamePlayConfig gamePlayConfig ) =>
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );

        public void MakeDecision( IPlayerReadOnly playerReadOnly, CancellationTokenSource cancellationTokenSource )
        {
            _isDecisionMade = false;
            OnNeedMakeDecision?.Invoke( playerReadOnly );
            StartTimerDecision( playerReadOnly, cancellationTokenSource ).Forget();
        }

        private async UniTaskVoid StartTimerDecision(
            IPlayerReadOnly playerReadOnly,
            CancellationTokenSource cancellationTokenSource
        )
        {
            _cancellationTokenDecision = new CancellationTokenSource();
            int seconds = _gamePlayConfig.WaitDecisionTimeInSec;
            OnTimerDecisionStarted?.Invoke( seconds );
            Debug.Log( "TIMER DECISION START" );
            await UniTask.Delay( TimeSpan.FromSeconds( seconds ), cancellationToken: cancellationTokenSource.Token );
            Debug.Log( "TIMER DECISION FINISH" );

            if ( cancellationTokenSource.IsCancellationRequested )
                return;

            if ( !_isDecisionMade )
            {
                OnTimerDecisionStopped?.Invoke();
                OnDecisionNotBeenMade?.Invoke( playerReadOnly );
            }
        }

        public void DecisionMade()
        {
            OnTimerDecisionStopped?.Invoke();
            _isDecisionMade = true;
            _cancellationTokenDecision.Cancel();
        }
    }
}