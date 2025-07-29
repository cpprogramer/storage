using MonopolySpace.Lobby;
using MonopolySpace.Model;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UIRollPanelController
    {
        private readonly UIRollPanelDecision _rollPanelDecision;
        private readonly UIRollPanelTurn _rollPanelTurn;
        private readonly IGamePlayManager _gamePlayManager;

        public UIRollPanelController(
            UIRollPanelTurn rollPanelTurn,
            UIRollPanelDecision rollPanelDecision,
            IGamePlayManager gamePlayManager
        )
        {
            _rollPanelTurn = rollPanelTurn;
            _rollPanelDecision = rollPanelDecision;
            _gamePlayManager = gamePlayManager;
            _gamePlayManager.WaitingPlayerMakeTurnHandler.OnNeedMakeTurn += NeedMakeTurnHandler;
            _gamePlayManager.WaitingPlayerMakeDecisionHandler.OnNeedMakeDecision += NeedMakeDecisionHandler;
        }

        private void NeedMakeDecisionHandler( IPlayerReadOnly playerReadOnly )
        {
            _rollPanelTurn.Show( false );
            _rollPanelDecision.Show( playerReadOnly.PlayerInfo.PlayerType == PlayerType.main );
        }

        private void NeedMakeTurnHandler( IPlayerReadOnly playerReadOnly )
        {
            _rollPanelDecision.Show( false );
            _rollPanelTurn.Show( playerReadOnly.PlayerInfo.PlayerType == PlayerType.main );
        }
    }
}