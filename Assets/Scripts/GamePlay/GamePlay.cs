using Common;
using MonopolySpace.Lobby;
using System;
using System.Threading;
using UnityEngine;

namespace MonopolySpace.Model
{
    public sealed class GamePlay : IGamePlay
    {
        public bool IsDisposed { get; private set; }
      
        public StartGameModel StartGameModel { get; }

      

        private IPlayersTurnSequenceHandler _playersTurnSequenceHandler;
        
        private readonly IPlayersHolder _playersHolder = new PlayersHolder();

        private readonly IWaitingPlayerMakeDecisionHandler _waitingPlayerMakeDecisionHandler;
        private readonly IWaitingPlayerMakeTurnHandler _waitingPlayerMakeTurnHandler;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public GamePlay(
            StartGameModel startGameModel,
            IWaitingPlayerMakeDecisionHandler waitingPlayerMakeDecisionHandler,
            IWaitingPlayerMakeTurnHandler waitingPlayerMakeTurnHandler
        )
        {
            StartGameModel = startGameModel ?? throw new ArgumentNullException( nameof(startGameModel) );
            _waitingPlayerMakeDecisionHandler = waitingPlayerMakeDecisionHandler ??
                                                throw new ArgumentNullException(
                                                    nameof(waitingPlayerMakeDecisionHandler) );
            _waitingPlayerMakeTurnHandler = waitingPlayerMakeTurnHandler ??
                                            throw new ArgumentNullException( nameof(waitingPlayerMakeTurnHandler) );

            _waitingPlayerMakeTurnHandler.OnTurnNotBeenMade += TurnNotBeenMadeHandler;

            _waitingPlayerMakeDecisionHandler.OnDecisionNotBeenMade += DecisionNotBeenMadeHandler;
        }

        private void NeedMakeTurnHandler( int indexPlayer )
        {
            IPlayer player = _playersHolder.GetPlayerByIndex( indexPlayer );
            _waitingPlayerMakeTurnHandler.MakeTurn( player, _cancellationTokenSource );
        }

        private void DecisionNotBeenMadeHandler( IPlayerReadOnly playerReadOnly )
        {
            AutoDecision();
            _playersTurnSequenceHandler.SwitchToNextPlayer();
            Start();
        }

        private void TurnNotBeenMadeHandler( IPlayerReadOnly playerReadOnly )
        {
            AutoTurn();
            _waitingPlayerMakeDecisionHandler.MakeDecision( playerReadOnly, _cancellationTokenSource );
        }

        public void Initialize()
        {
            Debug.Log( "GAME PLAY INITIALIZE" );

            //
            StartGameModel.Players.ForEach( playerInfo =>
            {
               // IPlayer player = _playerResolver.Resolve( playerInfo.PlayerType, playerInfo, default );
                //_playersHolder.AddPlayer( player );
            } );
            //

            _playersTurnSequenceHandler = new PlayersTurnSequenceHandler( StartGameModel.Players.Length, 0 );
            _playersTurnSequenceHandler.OnNeedMakeTurn += NeedMakeTurnHandler;
        }

        public void Dispose()
        {
            Debug.Log( "DISPOSE" );
            IsDisposed = true;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _playersTurnSequenceHandler.OnNeedMakeTurn -= NeedMakeTurnHandler;
            _waitingPlayerMakeTurnHandler.OnTurnNotBeenMade -= TurnNotBeenMadeHandler;
            _waitingPlayerMakeDecisionHandler.OnDecisionNotBeenMade -= DecisionNotBeenMadeHandler;
        }

        public void Start()
        {
            if ( IsDisposed )
                return;

            Debug.Log( "START" );
            _playersTurnSequenceHandler.Start();
        }

        private void AutoTurn() {}

        private void AutoDecision() {}
    }
}