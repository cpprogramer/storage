using System;
using Common;
using Common.Profile;
using Common.UI;
using Configs;
using FSM;
using GameFSM;
using MonopolySpace.Lobby;
using MonopolySpace.Messages;
using MonopolySpace.Net;
using MonopolySpace.Profile;
using MonopolySpace.UI.Controllers;
using UniRx;
using UnityEngine;

namespace MonopolySpace
{
    public sealed class GameRoot
    {
        private readonly int _instanceUid;
        private readonly bool _isDebugMode;
        private readonly IParentHolder _parentHolder;
        private readonly ITickable _tickable;
        private IFSM _fsm;
        private IGamePlayConfig _gamePlayConfig;
        private IMessageBroker _messageBroker;
        private IMultiplayerService _multiplayerService;
        private IScenesManager _scenesManager;
        private IStartGameService _startGameService;
        private IUiRootAggregator _uiRootAggregator;
        private IUiRootViewModel _uiRootViewModel;
        private IUserProfile _userProfile;

        public GameRoot(int instanceUid, ITickable tickable, IParentHolder parentHolder, bool isDebugMode)
        {
            _instanceUid = instanceUid;
            _tickable = tickable ?? throw new ArgumentNullException(nameof(tickable));
            _parentHolder = parentHolder ?? throw new ArgumentNullException(nameof(parentHolder));
            _isDebugMode = isDebugMode;
        }

        public event Action OnInitialized;

        public void Run()
        {
            _fsm.SetState(typeof(LoadingFSMObject), DummyModel.Dummy);
        }

        public void Create()
        {
            _messageBroker = new MessageBroker();
            _scenesManager = new ScenesManager();
            _gamePlayConfig = Resources.Load<GamePlayConfig>(Consts.PATH_TO_CONFIG);
            _userProfile = new UserProfile(_messageBroker);
            _uiRootViewModel = new UiRootViewModel(_instanceUid, _isDebugMode, _parentHolder, _messageBroker);
            _uiRootAggregator = new UiRootAggregator(_instanceUid, _uiRootViewModel, _messageBroker);

            _multiplayerService =
                new MultiplayerService(
                    new PhotonMultiplayerBackend(_tickable, _messageBroker, new VersionNumberProvider()),
                    _messageBroker);


            _fsm = new GameFsm();
            _fsm.RegisterState(typeof(GameFSMObject), model =>
                new GameFSMObject(_instanceUid, _fsm, _scenesManager, model, _gamePlayConfig, _messageBroker));
            _fsm.RegisterState(typeof(MainMenuFSMObject), model =>
                new MainMenuFSMObject(_fsm, _startGameService, _multiplayerService, _messageBroker));
            _fsm.RegisterState(typeof(WaitPlayersFSMObject),
                model => new WaitPlayersFSMObject(_fsm, _multiplayerService));
            _fsm.RegisterState(typeof(LobbyFSMObject),
                model => new LobbyFSMObject(_fsm, _messageBroker, _multiplayerService));
            _fsm.RegisterState(typeof(LoadingFSMObject), model => new LoadingFSMObject(_fsm, _messageBroker));

            _startGameService = new StartGameService(_fsm);

            AddWindows();
        }

        public void Initialize()
        {
            _uiRootViewModel.OnInitialized += InitializedHandler;
            _uiRootViewModel.Initialize();

            void InitializedHandler()
            {
                _uiRootViewModel.OnInitialized -= InitializedHandler;
                OnInitialized?.Invoke();
            }
        }

        private void AddWindows()
        {
            _uiRootViewModel.RegisterWindow(typeof(UIGameController),
                () => new UIGameController(_uiRootAggregator, _gamePlayConfig));
            _uiRootViewModel.RegisterWindow(typeof(UIMainMenuController),
                () => new UIMainMenuController(_uiRootAggregator, _userProfile));
            _uiRootViewModel.RegisterWindow(typeof(UILoadingController),
                () => new UILoadingController(_uiRootAggregator));
            _uiRootViewModel.RegisterWindow(typeof(UIWaitPlayersController),
                () => new UIWaitPlayersController(_uiRootAggregator, _gamePlayConfig));
            _uiRootViewModel.RegisterWindow(typeof(UINetLobbyController),
                () => new UINetLobbyController(_uiRootAggregator, _multiplayerService));
        }
    }
}