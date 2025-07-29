using Common.UI;
using Common.UI.Model;
using Configs;
using System;

namespace MonopolySpace.UI.Controllers
{
    public class CustomAttribute : Attribute
    {
        public Type AttributeType;
        public CustomAttribute( Type attributeType )
        {
            AttributeType = attributeType;
        }
    }
    
    [CustomAttribute(typeof(UIGameController))]
    public class UIGameController : Window< UIGameView >
    {
        private readonly IGamePlayConfig _gamePlayConfig;
        private GameWindowModel _model;
        private UIPlayerSlotsController _playerSlotsController;
        private UIRollPanelController _rollPanelController;

        public UIGameController(
            IUiRootAggregator uiRootAggregator,
            IGamePlayConfig gamePlayConfig,
            bool isModal = false
        ) : base( uiRootAggregator ) =>
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );

        protected override void OnInitialize( BaseWindowModel model )
        {
            _model = (GameWindowModel)model;
            _playerSlotsController = new UIPlayerSlotsController( _baseView.PlayerSlotsView,
                _model.GamePlayManager.GamePlayReadOnly.StartGameModel.Players,
                _model.GamePlayManager.WaitingPlayerMakeDecisionHandler,
                _model.GamePlayManager.WaitingPlayerMakeTurnHandler );

            _baseView.OnRoll += RollHandler;
            _baseView.OnClose += CloseHandler;
            _rollPanelController = new UIRollPanelController( _baseView.RollPanelTurn, _baseView.RollPanelDecision,
                _model.GamePlayManager );
        }

        private void RollHandler( RollDataMessage data ) => _uiRootAggregator.MessageBroker.Publish( data );

        private void CloseHandler() => _uiRootAggregator.MessageBroker.Publish( new ExitFromGameMessage() );
    }
}