namespace StorageTest.Model
{
    public interface IPlayersHolder
    {
        IPlayer MainPlayer { get; }

        void AddPlayer( IPlayer player );
        void RemovePlayer( IPlayer player );
        IPlayer GetPlayerByIndex( int indexPlayer );
    }
}