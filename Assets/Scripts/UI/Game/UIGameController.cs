using System;
using Common.UI;
using Common.UI.Model;
using Configs;

namespace StorageTest.UI.Controllers
{
    public class UIGameController : UIBaseViewModel<UIGameView>
    {
        private readonly IGamePlayConfig _gamePlayConfig;
        private GameWindowDTO _dto;


        public UIGameController(
            IUiRootAggregator uiRootAggregator,
            IGamePlayConfig gamePlayConfig
        ) : base(uiRootAggregator)
        {
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException(nameof(gamePlayConfig));
        }

        protected override void OnInitialize(BaseWindowDTO dto)
        {
            _dto = (GameWindowDTO)dto;
            _baseView.OnClose += CloseHandler;
        }

        private void CloseHandler()
        {
            _uiRootAggregator.MessageBroker.Publish(new ExitFromGameMessage());
        }
    }
}