using StorageTest.Lobby;

namespace StorageTest.Model
{
    public sealed class Player : BasePlayer
    {
        private readonly IGamePlayManager _gamePlayManager;

        public Player( PlayerInfo info, IGamePlayManager gamePlayManager ) : base( info ) =>
            _gamePlayManager = gamePlayManager;
    }
}