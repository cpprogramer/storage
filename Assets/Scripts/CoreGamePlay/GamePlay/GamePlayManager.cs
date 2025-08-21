using Common;
using Common.UI;
using Common.UI.Messages;
using Configs;
using Cysharp.Threading.Tasks;
using StorageTest.UI.Controllers;
using System;
using System.Collections.Generic;
using StorageTest.Lobby;
using StorageTest.Messages;
using StorageTest.View;
using UniRx;
using UnityEngine;

namespace StorageTest.Model
{
    public sealed class GamePlayManager : IGamePlayManager, IDisposable
    {
        public IGamePlayReadOnly GamePlayReadOnly => _gamePlay;
     
        private readonly IGamePlayConfig _gamePlayConfig;
        private readonly StartGameModel _model;
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
            StartGameModel model
        )
        {
            _instanceUid = instanceUid;
            _scenesManager = scenesManager ?? throw new ArgumentNullException( nameof(scenesManager) );
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );

            _gamePlayConfig = gamePlayConfig;
            _model = model;
            
           /* var container = new Dictionary< PlayerType, Func< PlayerInfo, IGamePlayManager, IPlayer > >
            {
                { PlayerType.bot, ( playerInfo, context ) => new BotPlayer( playerInfo ) },
                { PlayerType.main, ( playerInfo, context ) => new Player( playerInfo, context ) },
                { PlayerType.other, ( playerInfo, context ) => new OtherPlayer( playerInfo ) }
            };*/
            
        }

        private void LevelLoadedHandler() => _gameView.OnLevelLoaded -= LevelLoadedHandler;

        public void Dispose()
        {
            _messageBroker.Publish( new LeaveRoomMessage() );
            _gamePlay.Dispose();
            _gameView.Dispose();
        }

        public void Create()
        {
            _gamePlay = new GamePlay( _model );
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
    }
}