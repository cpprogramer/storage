using System;
using Common.UI;
using Common.UI.Model;
using Configs;

namespace StorageTest.UI.Controllers
{
    public class CustomAttribute : Attribute
    {
        public Type AttributeType;

        public CustomAttribute(Type attributeType)
        {
            AttributeType = attributeType;
        }
    }

    [Custom(typeof(UIGameController))]
    public class UIGameController : Window<UIGameView>
    {
        private readonly IGamePlayConfig _gamePlayConfig;
        private GameWindowModel _model;


        public UIGameController(
            IUiRootAggregator uiRootAggregator,
            IGamePlayConfig gamePlayConfig,
            bool isModal = false
        ) : base(uiRootAggregator)
        {
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException(nameof(gamePlayConfig));
        }

        protected override void OnInitialize(BaseWindowModel model)
        {
            _model = (GameWindowModel)model;
            _baseView.OnClose += CloseHandler;
        }

        private void CloseHandler()
        {
            _uiRootAggregator.MessageBroker.Publish(new ExitFromGameMessage());
        }
    }
}