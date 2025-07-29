using MonopolySpace.Lobby;
using MonopolySpace.Model;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UIPlayerSlotsController
    {
        private readonly UIPlayerSlotsView _playerSlotsView;
        private readonly IWaitingPlayerMakeDecisionHandler _waitingPlayerMakeDecisionHandler;
        private readonly IWaitingPlayerMakeTurnHandler _waitingPlayerMakeTurnHandler;

        public UIPlayerSlotsController(
            UIPlayerSlotsView playerSlotsView,
            PlayerInfo[] players,
            IWaitingPlayerMakeDecisionHandler waitingPlayerMakeDecisionHandler,
            IWaitingPlayerMakeTurnHandler waitingPlayerMakeTurnHandler
        )
        {
            _playerSlotsView = playerSlotsView;
            _playerSlotsView.Setup( players );
            _waitingPlayerMakeDecisionHandler = waitingPlayerMakeDecisionHandler;
            _waitingPlayerMakeTurnHandler = waitingPlayerMakeTurnHandler;

            _waitingPlayerMakeDecisionHandler.OnNeedMakeDecision += NeedMakeDecisionHandler;
            _waitingPlayerMakeTurnHandler.OnNeedMakeTurn += NeedMakeTurnHandler;

            _waitingPlayerMakeDecisionHandler.OnTimerDecisionStarted += TimerDecisionStartedHandler;
            _waitingPlayerMakeDecisionHandler.OnTimerDecisionStopped += TimerDecisionStoppedHandler;

            _waitingPlayerMakeTurnHandler.OnTimerTurnStarted += TimerTurnStartedHandler;
            _waitingPlayerMakeTurnHandler.OnTimerTurnStopped += TimerTurnStoppedHandler;
        }

        private void TimerDecisionStartedHandler( float time ) => _playerSlotsView.StartProgress( time );

        private void TimerDecisionStoppedHandler() => _playerSlotsView.StopProgress();

        private void TimerTurnStartedHandler( float time ) => _playerSlotsView.StartProgress( time );

        private void TimerTurnStoppedHandler() => _playerSlotsView.StopProgress();

        private void NeedMakeDecisionHandler( IPlayerReadOnly player ) => _playerSlotsView.MarkPlayerAsActive( player );

        private void NeedMakeTurnHandler( IPlayerReadOnly player ) => _playerSlotsView.MarkPlayerAsActive( player );
    }
}