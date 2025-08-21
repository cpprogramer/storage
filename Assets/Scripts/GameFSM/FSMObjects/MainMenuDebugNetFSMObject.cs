using System;
using Common;
using Common.UI.Messages;
using Cysharp.Threading.Tasks;
using StorageTest.Lobby;
using StorageTest.Messages;
using StorageTest.Net;
using StorageTest.UI.Controllers;
using UniRx;

namespace FSM
{
    public sealed class MainMenuDebugNetFSMObject : BaseFiniteStateMachineObject
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IMultiplayerService _multiplayerService;
        private readonly IStartGameService _startGameService;
        private CompositeDisposable _compositeDisposable;

        public MainMenuDebugNetFSMObject(
            IFSM parentFsm,
            IStartGameService startGameService,
            IMultiplayerService multiplayerService,
            IMessageBroker messageBroker
        ) : base(parentFsm)
        {
            _startGameService = startGameService ?? throw new ArgumentNullException(nameof(startGameService));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _multiplayerService = multiplayerService ?? throw new ArgumentNullException(nameof(multiplayerService));
        }

        /* private void TestEventDeleteLaterHandler()
         {
             _multiplayerBackend.OnTestEventDeleteLater -= TestEventDeleteLaterHandler;
             var players = new[]
             {
                 new PlayerInfo( "0", PlayerType.main ),
                 new PlayerInfo( "1", PlayerType.bot ),
                 new PlayerInfo( "2", PlayerType.bot ),
                 new PlayerInfo( "3", PlayerType.bot )
             };

             _startGameService.StartGame( new StartGameModel( Consts.TESTGAMEPLAY, "TestLevel", players ) );
         }*/

        private void UIConnectToLobbyMessageHandler(UIConnectToLobbyMessage msg)
        {
            TryConnectAndJoinToLobby().Forget();
        }

        public override void Dispose()
        {
            _compositeDisposable.Dispose();
            //_uiManager.CloseWindow( new UICloseWindowMessage( typeof(UIMainMenuController), WindowResult.Back ) );
        }

        private async UniTaskVoid TryConnectAndJoinToLobby()
        {
            await _multiplayerService.ConnectAsync();
            if (!_multiplayerService.IsConnectedAndReady)
                //TODO диалог
                //_uiManager.ShowWindow( new UIOpenWindowMessage( new UIMessageBoxModel() ) );
                return;

            await _multiplayerService.JoinLobbyAsync();
            if (!_multiplayerService.InLobby)
                //TODO диалог
                //_uiManager.ShowWindow( new UIOpenWindowMessage( new UIMessageBoxModel() ) );
                return;

            _parentFsm.SetState(typeof(NetLobbyFSMObject));
        }

        protected override void OnInitialize()
        {
            _compositeDisposable = new CompositeDisposable();
            _messageBroker.Receive<UIConnectToLobbyMessage>().Subscribe(UIConnectToLobbyMessageHandler)
                .AddTo(_compositeDisposable);
            _messageBroker.Publish(new UIOpenWindowMessage(new UIMainMenuModel()));
        }
    }
}