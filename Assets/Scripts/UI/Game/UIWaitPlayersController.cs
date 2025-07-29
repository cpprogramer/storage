using Common.UI;
using Common.UI.Model;
using Configs;
using System;
using System.Threading;
using UniRx;

namespace MonopolySpace.UI.Controllers
{
    public class UIWaitPlayersController : Window< UIWaitPlayersView >
    {
        private readonly IGamePlayConfig _gamePlayConfig;
        private UIWaitPlayersModel _model;
        private CancellationTokenSource _cancellationTokenSource;
        private UIPlayerSlotsController _playerSlotsController;
        private UIRollPanelController _rollPanelController;
        private CompositeDisposable _compositeDisposable;

        public UIWaitPlayersController(
            IUiRootAggregator uiRootAggregator,
            IGamePlayConfig gamePlayConfig,
            bool isModal = false
        ) : base( uiRootAggregator ) =>
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );

        protected override void OnInitialize( BaseWindowModel model )
        {
            _compositeDisposable = new CompositeDisposable();
            _cancellationTokenSource = new CancellationTokenSource();

            _model = (UIWaitPlayersModel)model;
            _baseView.Setup( 0, 4 );
        }
    }
}