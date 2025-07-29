using System;
using System.Threading;
using Common.Profile;
using Common.UI;
using Common.UI.Model;
using Configs;
using MonopolySpace.Messages;
using MonopolySpace.UI.View;
using UniRx;
using UnityEngine;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UIMainMenuController : Window<UIMainMenuView>
    {
        private readonly IConsumablesConfig _consumablesConfig;
        private readonly IUserProfile _userProfile;
        private CancellationTokenSource _cancellationTokenSource;
        private CompositeDisposable _compositeDisposable = new();
        private UIMainMenuModel _model;

        public UIMainMenuController(IUiRootAggregator uiRootAggregator, IUserProfile userProfile)
            : base(uiRootAggregator)
        {
            _userProfile = userProfile ?? throw new ArgumentNullException(nameof(userProfile));
            _consumablesConfig = Resources.Load<ConsumablesConfig>("Configs/ConsumablesConfig");
        }

        protected override void OnInitialize(BaseWindowModel model)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _compositeDisposable = new CompositeDisposable();
            _model = (UIMainMenuModel)model;
            _baseView.OnPlayClicked += PlayClickedHandler;
        }

        protected override void OnClosing()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _compositeDisposable.Dispose();
        }

        private void PlayClickedHandler()
        {
            _uiRootAggregator.MessageBroker.Publish(new UIConnectToLobbyMessage());
        }
    }
}