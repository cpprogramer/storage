using Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace MonopolySpace.Model
{
    public sealed class WaitingPlayerMakeTurnHandler : IWaitingPlayerMakeTurnHandler
    {
        public event Action< IPlayerReadOnly > OnNeedMakeTurn;

        public event Action< float > OnTimerTurnStarted;
        public event Action OnTimerTurnStopped;
        public event Action< IPlayerReadOnly > OnTurnNotBeenMade;

        private readonly IGamePlayConfig _gamePlayConfig;

        private CancellationTokenSource _cancellationTokenTurn;

        private bool _isTurnMade;

        public WaitingPlayerMakeTurnHandler( IGamePlayConfig gamePlayConfig ) =>
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );

        public void MakeTurn( IPlayerReadOnly playerReadOnly, CancellationTokenSource cancellationTokenSource )
        {
            _isTurnMade = false;
            OnNeedMakeTurn?.Invoke( playerReadOnly );
            StartTimerTurn( playerReadOnly, cancellationTokenSource ).Forget();
        }

        private async UniTaskVoid StartTimerTurn(
            IPlayerReadOnly playerReadOnly,
            CancellationTokenSource cancellationTokenSource
        )
        {
            _cancellationTokenTurn = new CancellationTokenSource();
            int seconds = _gamePlayConfig.WaitMakeTurnTimeInSec;
            OnTimerTurnStarted?.Invoke( seconds );
            Debug.Log( "TIMER TURN START" );
            await UniTask.Delay( TimeSpan.FromSeconds( seconds ), cancellationToken: cancellationTokenSource.Token );
            Debug.Log( $"TIMER TURN FINISH canceled?:{cancellationTokenSource.IsCancellationRequested}" );
            if ( cancellationTokenSource.IsCancellationRequested )
                return;

            if ( !_isTurnMade )
            {
                OnTimerTurnStopped?.Invoke();
                OnTurnNotBeenMade?.Invoke( playerReadOnly );
            }
        }

        public void TurnMade()
        {
            OnTimerTurnStopped?.Invoke();
            _isTurnMade = true;
            _cancellationTokenTurn.Cancel();
        }
    }
}