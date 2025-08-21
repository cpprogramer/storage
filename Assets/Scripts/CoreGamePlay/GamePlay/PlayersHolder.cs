using System.Collections.Generic;
using StorageTest.Lobby;

namespace StorageTest.Model
{
    public sealed class PlayersHolder : IPlayersHolder
    {
        public IPlayer MainPlayer { get; private set; }
        private readonly Dictionary< string, IPlayer > _players = new();
        private readonly List< IPlayer > _playersArray = new();

        void IPlayersHolder.AddPlayer( IPlayer player )
        {
            _players.Add( player.PlayerInfo.Uid, player );
            _playersArray.Add( player );
            if ( player.PlayerInfo.PlayerType == PlayerType.main ) MainPlayer = player;
        }

        void IPlayersHolder.RemovePlayer( IPlayer player )
        {
            _players.Remove( player.PlayerInfo.Uid );
            _playersArray.Remove( player );
        }

        IPlayer IPlayersHolder.GetPlayerByIndex( int indexPlayer )
        {
            if ( indexPlayer < 0 || indexPlayer >= _playersArray.Count ) return default;

            return _playersArray[ indexPlayer ];
        }
    }
}