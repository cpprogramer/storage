namespace StorageTest.Lobby
{
    public enum PlayerType
    {
        main,
        other,
        bot
    }

    public sealed class PlayerInfo
    {
        public string Uid { get; }
        public PlayerType PlayerType { get; }

        public PlayerInfo( string uid, PlayerType playerType )
        {
            Uid = uid;
            PlayerType = playerType;
        }
    }
}