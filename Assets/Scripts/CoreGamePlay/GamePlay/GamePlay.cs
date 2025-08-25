using StorageTest.Lobby;
using System;
using System.Threading;
using UnityEngine;

namespace StorageTest.Model
{
    public sealed class GamePlay : IGamePlay
    {
        public bool IsDisposed { get; private set; }

        public StartGameDTO StartGameDto { get; }
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly IPlayersHolder _playersHolder = new PlayersHolder();

        public GamePlay( StartGameDTO startGameDto ) =>
            StartGameDto = startGameDto ?? throw new ArgumentNullException( nameof(startGameDto) );

        public void Initialize()
        {
            Debug.Log( "GAME PLAY INITIALIZE" );

            //
            StartGameDto.Players.ForEach( playerInfo =>
            {
                // IPlayer player = _playerResolver.Resolve( playerInfo.PlayerType, playerInfo, default );
                //_playersHolder.AddPlayer( player );
            } );
            //
        }

        public void Dispose()
        {
            Debug.Log( "DISPOSE" );
            IsDisposed = true;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public void Start()
        {
            if ( IsDisposed )
                return;

            Debug.Log( "START" );
        }
    }
}