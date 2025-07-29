using Common;
using Common.UI;
using Common.UI.Messages;
using Configs;
using Cysharp.Threading.Tasks;
using MonopolySpace.Lobby;
using MonopolySpace.Messages;
using MonopolySpace.UI.Controllers;
using MonopolySpace.View;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MonopolySpace.Model
{
    public sealed class GamePlayManager : IGamePlayManager, IDisposable
    {
        public IGamePlayReadOnly GamePlayReadOnly => _gamePlay;
        public IWaitingPlayerMakeDecisionHandler WaitingPlayerMakeDecisionHandler { get; }

        public IWaitingPlayerMakeTurnHandler WaitingPlayerMakeTurnHandler { get; }

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
            WaitingPlayerMakeDecisionHandler = new WaitingPlayerMakeDecisionHandler( _gamePlayConfig );
            WaitingPlayerMakeTurnHandler = new WaitingPlayerMakeTurnHandler( _gamePlayConfig );

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
            _gamePlay = new GamePlay( _model,  WaitingPlayerMakeDecisionHandler,
                WaitingPlayerMakeTurnHandler );
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