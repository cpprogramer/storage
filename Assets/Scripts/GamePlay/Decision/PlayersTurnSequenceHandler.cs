using System;
using UnityEngine;

namespace MonopolySpace.Model
{
    public sealed class PlayersTurnSequenceHandler : IPlayersTurnSequenceHandler
    {
        public event Action< int > OnNeedMakeTurn;
        private readonly int _countPlayers;
        private int _indexCurrentActivePlayer;

        public PlayersTurnSequenceHandler( int countPlayers, int indexCurrentActivePlayer )
        {
            _countPlayers = countPlayers;
            if ( indexCurrentActivePlayer >= countPlayers )
                throw new Exception(
                    $"{nameof(PlayersTurnSequenceHandler)} indexCurrentActivePlayer >= players.Length" );
            _indexCurrentActivePlayer = indexCurrentActivePlayer;
        }

        public void SwitchToNextPlayer() =>
            _indexCurrentActivePlayer = (int)Mathf.Repeat( _indexCurrentActivePlayer + 1, _countPlayers );

        public void Start() => OnNeedMakeTurn?.Invoke( _indexCurrentActivePlayer );
    }
}