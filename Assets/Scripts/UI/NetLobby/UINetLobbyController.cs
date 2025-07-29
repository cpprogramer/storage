using Common.UI;
using Common.UI.Messages;
using Common.UI.Model;
using MonopolySpace.Net;
using MonopolySpace.UI.View;
using System;
using System.Threading;
using UniRx;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UINetLobbyController : Window< UINetLobbyView >
    {
        private UINetLobbyModel _model;
        private readonly IMultiplayerService _multiplayerService;

        private CompositeDisposable _compositeDisposable = new();
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isClosed;

        public UINetLobbyController( IUiRootAggregator uiRootAggregator, IMultiplayerService multiplayerService ) :
            base( uiRootAggregator )
        {
            _multiplayerService = multiplayerService ?? throw new ArgumentNullException( nameof(multiplayerService) );
        }

        protected override void OnInitialize( BaseWindowModel model )
        {
            _isClosed = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _compositeDisposable = new CompositeDisposable();
            _model = (UINetLobbyModel)model;
            _baseView.OnExitClicked += ExitClickedHandler;
            _baseView.OnJoinOrCreateRoomClicked += JoinOrCreateRoomClickedHandler;
            _multiplayerService.OnRoomListUpdate += RoomListUpdateHandler;
            _baseView.Setup( _multiplayerService.CachedRooms );
        }

        protected override void OnClosing()
        {
            _multiplayerService.OnRoomListUpdate -= RoomListUpdateHandler;
            _isClosed = true;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _compositeDisposable.Dispose();
        }

        private void ExitClickedHandler() => _uiRootAggregator.MessageBroker.Publish( new UIExitFromLobbyToMainMenuMessage() );

        private void RoomListUpdateHandler( IRoomInfo[] roomInfos ) => _baseView.Setup( roomInfos );

        private void JoinOrCreateRoomClickedHandler() => _uiRootAggregator.MessageBroker.Publish( new UIJoinOrCreateRoomMessage() );
    }
}