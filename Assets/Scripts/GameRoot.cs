using Common;
using Common.Profile;
using Common.UI;
using Common.UI.Messages;
using Common.UI.Model;
using Configs;
using Cysharp.Threading.Tasks;
using FSM;
using GameFSM;
using StorageTest.Lobby;
using StorageTest.Messages;
using StorageTest.Net;
using StorageTest.Profile;
using StorageTest.UI.Controllers;
using System;
using UniRx;
using UnityEngine;

namespace StorageTest
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
        private IUiManager< Type, WindowResult > _uiManager;

        public GameRoot( int instanceUid, ITickable tickable, IParentHolder parentHolder, bool isDebugMode )
        {
            _instanceUid = instanceUid;
            _tickable = tickable ?? throw new ArgumentNullException( nameof(tickable) );
            _parentHolder = parentHolder ?? throw new ArgumentNullException( nameof(parentHolder) );
            _isDebugMode = isDebugMode;
        }

        public void Run() => _fsm.SetState( typeof(LoadingFSMObject), DummyModel.Dummy );

        public void Create()
        {
            _messageBroker = new MessageBroker();
            _scenesManager = new ScenesManager();
            _gamePlayConfig = Resources.Load< GamePlayConfig >( Consts.PATH_TO_CONFIG );
            _userProfile = new UserProfile( _messageBroker );
            var uiManager = new UiManager( _messageBroker );
            _uiManager = uiManager;
            _uiRootViewModel = new UiRootViewModel( _instanceUid, _isDebugMode, _parentHolder );
            _uiRootAggregator = new UiRootAggregator( _instanceUid, _uiRootViewModel, _messageBroker, uiManager );

            _multiplayerService =
                new MultiplayerService(
                    new PhotonMultiplayerBackend( _tickable, _messageBroker, new VersionNumberProvider() ),
                    _messageBroker );

            _fsm = new GameFsm();
            _startGameService = new StartGameService( _fsm );

            AddFSM();
            AddWindows();
        }
        
        private void AddFSM()
        {
            _fsm.RegisterState( typeof(GameFSMObject),
                model => new GameFSMObject( _instanceUid, _fsm, _scenesManager, model, _gamePlayConfig,
                    _messageBroker ) );

            _fsm.RegisterState( typeof(MainMenuFSMObject),
                _ => new MainMenuFSMObject( _fsm, _startGameService, _messageBroker ) );

            _fsm.RegisterState( typeof(WaitPlayersFSMObject),
                _ => new WaitPlayersFSMObject( _fsm, _multiplayerService ) );

            _fsm.RegisterState( typeof(NetLobbyFSMObject),
                _ => new NetLobbyFSMObject( _fsm, _messageBroker, _multiplayerService ) );

            _fsm.RegisterState( typeof(LoadingFSMObject), _ => new LoadingFSMObject( _fsm, _messageBroker ) );

            _fsm.RegisterState( typeof(HangarFSMObject),
                _ => new HangarFSMObject( _fsm, _messageBroker, _scenesManager, _instanceUid ) );
        }

        public async UniTask InitializeAsync()
        {
            await _uiRootViewModel.InitializeAsync();
        }

        private void AddWindows()
        {
            _uiManager.RegisterWindow( typeof(UIGameController),
                () => new UIGameController( _uiRootAggregator, _gamePlayConfig ) );
            
            _uiManager.RegisterWindow( typeof(UIMainMenuController),
                () => new UIMainMenuController( _uiRootAggregator, _userProfile ) );
            
            _uiManager.RegisterWindow( typeof(UILoadingController),
                () => new UILoadingController( _uiRootAggregator ) );
            
            _uiManager.RegisterWindow( typeof(UIHangarController),
                () => new UILoadingController( _uiRootAggregator ) );
            
            _uiManager.RegisterWindow( typeof(UINetLobbyController),
                () => new UINetLobbyController( _uiRootAggregator, _multiplayerService ) );
        }
        
        #if UNITY_EDITOR

        public void TestFSM()
        {
            var types = Utils.GetDerivedTypes( typeof(BaseFiniteStateMachineObject) );
            
            types.ForEach(item=>
            {
                _fsm.SetState( item );
            } );

        }

        public void TestUI()
        {
            var types = Utils.GetDerivedTypes( typeof(BaseWindowDTO) );
            
            types.ForEach(item=>
            {
                var dto = Activator.CreateInstance( item );
                _messageBroker.Publish( new UIOpenWindowMessage(dto as BaseWindowDTO ) );
            } );

            Debug.LogError( "[+] 1" );
            Observable.Timer( System.TimeSpan.FromSeconds( 5 ) ).Distinct().Subscribe( _ =>
            {
                Debug.LogError( "[+] 2" );
                
                types.ForEach(item=>
                {
                    var dto = Activator.CreateInstance( item );
                    _messageBroker.Publish( new UICloseWindowMessage( dto.GetType(), WindowResult.Back ) );
                } );
            } );
        }
        
        #endif
    }
}