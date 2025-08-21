using System;
using System.Threading;
using Common.Profile;
using Common.UI;
using Common.UI.Model;
using Configs;
using StorageTest.Messages;
using StorageTest.UI.View;
using UniRx;
using UnityEngine;

namespace StorageTest.UI.Controllers
{
    public sealed class UIMainMenuController : Window<UIMainMenuView>
    {
        private readonly IConsumablesConfig _consumablesConfig;
        private readonly IUserProfile _userProfile;
      
        private UIMainMenuModel _model;

        public UIMainMenuController(IUiRootAggregator uiRootAggregator, IUserProfile userProfile)
            : base(uiRootAggregator)
        {
            _userProfile = userProfile ?? throw new ArgumentNullException(nameof(userProfile));
            _consumablesConfig = Resources.Load<ConsumablesConfig>("Configs/ConsumablesConfig");
        }

        protected override void OnInitialize(BaseWindowModel model)
        {
            _model = (UIMainMenuModel)model;
            _baseView.OnPlayClicked += PlayClickedHandler;
        }

        protected override void OnClosing()
        {
        }

        private void PlayClickedHandler()
        {
            _uiRootAggregator.MessageBroker.Publish(new GotoHangarMessage());
        }
    }
}