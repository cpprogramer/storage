using Common;
using Common.Models;
using Common.UI;
using Configs;
using Cysharp.Threading.Tasks;
using StorageTest.Lobby;
using StorageTest.Model;
using StorageTest.UI;
using System;
using UniRx;

namespace FSM
{
    public sealed class GameFSMObject : BaseFiniteStateMachineObject
    {
        private readonly IGamePlayConfig _gamePlayConfig;
        private readonly StartGameDTO _dto;
        private readonly IScenesManager _scenesManager;
        private readonly IMessageBroker _messageBroker;
        private GamePlayManager _playManager;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly int _instanceUid;

        public GameFSMObject(
            int instanceUid,
            IFSM parentFsm,
            IScenesManager scenesManager,
            IBaseDTO baseDto,
            IGamePlayConfig gamePlayConfig,
            IMessageBroker messageBroker
        ) : base( parentFsm )
        {
            _instanceUid = instanceUid;
            _gamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );
            _scenesManager = scenesManager ?? throw new ArgumentNullException( nameof(scenesManager) );
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );

            _dto = baseDto as StartGameDTO;
            _messageBroker.Receive< ExitFromGameMessage >().Subscribe( ExitFromGameMessageHandler )
                .AddTo( _compositeDisposable );
        }

        protected override void OnStart() => _playManager.Start();

        protected override void OnInitialize() => _playManager.Initialize();

        private void ExitFromGameMessageHandler( ExitFromGameMessage message ) =>
            _parentFsm.SetState( typeof(MainMenuFSMObject) );

        public override void Dispose()
        {
            _playManager.Dispose();
            _compositeDisposable.Dispose();
        }

        protected override void OnCreate()
        {
            _playManager = new GamePlayManager( _instanceUid, _messageBroker, _scenesManager, _gamePlayConfig, _dto );
            _playManager.Create();
        }
    }
}