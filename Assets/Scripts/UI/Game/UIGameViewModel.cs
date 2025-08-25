using Common.UI;
using Common.UI.Model;
using Configs;
using System;
using UI.Messages;

namespace UI.Game
{
    public class UIGameViewModel : UIBaseViewModel< UIGameView >
    {
        private readonly IGamePlayConfig _gamePlayConfig;
        private GameWindowDTO _dto;

        public UIGameViewModel( IUiRootAggregator uiRootAggregator, IGamePlayConfig gamePlayConfig ) :
            base( uiRootAggregator ) =>
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );

        protected override void OnInitialize( BaseWindowDTO dto )
        {
            _dto = (GameWindowDTO)dto;
            _baseView.OnClose += CloseHandler;
        }

        private void CloseHandler() => _uiRootAggregator.MessageBroker.Publish( new ExitFromGameMessage() );
    }
}