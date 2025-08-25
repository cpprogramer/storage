using Common;
using Configs;
using StorageTest.Lobby;
using StorageTest.Messages;
using StorageTest.View;
using System;
using UniRx;

namespace StorageTest.Model
{
    public sealed class GamePlayManager : IGamePlayManager, IDisposable
    {
        public IGamePlayReadOnly GamePlayReadOnly => _gamePlay;

        private readonly IGamePlayConfig _gamePlayConfig;
        private readonly StartGameDTO _dto;
        private readonly IScenesManager _scenesManager;
        private IGamePlay _gamePlay;
        private IGameView _gameView;
        private readonly int _instanceUid;
        private readonly IMessageBroker _messageBroker;

        public GamePlayManager(
            int instanceUid,
            IMessageBroker messageBroker,
            IScenesManager scenesManager,
            IGamePlayConfig gamePlayConfig,
            StartGameDTO dto
        )
        {
            _instanceUid = instanceUid;
            _scenesManager = scenesManager ?? throw new ArgumentNullException( nameof(scenesManager) );
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );

            _gamePlayConfig = gamePlayConfig;
            _dto = dto;

            /* var container = new Dictionary< PlayerType, Func< PlayerInfo, IGamePlayManager, IPlayer > >
             {
                 { PlayerType.bot, ( playerInfo, context ) => new BotPlayer( playerInfo ) },
                 { PlayerType.main, ( playerInfo, context ) => new Player( playerInfo, context ) },
                 { PlayerType.other, ( playerInfo, context ) => new OtherPlayer( playerInfo ) }
             };*/
        }

        public void Dispose()
        {
            _messageBroker.Publish( new LeaveRoomMessage() );
            _gamePlay.Dispose();
            _gameView.Dispose();
        }

        public void Create()
        {
            _gamePlay = new GamePlay( _dto );
            _gameView = new GameView( _instanceUid, _scenesManager, _gamePlay );
        }

        public void Initialize()
        {
            _gamePlay.Initialize();
            _gameView.OnLevelLoaded += LevelLoadedHandler;
            _gameView.Initialize();
        }

        public void Start()
        {
            _gameView.Start();
            _gamePlay.Start();
        }

        private void LevelLoadedHandler() => _gameView.OnLevelLoaded -= LevelLoadedHandler;
    }
}