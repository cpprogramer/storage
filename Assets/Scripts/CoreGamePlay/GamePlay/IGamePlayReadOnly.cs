using StorageTest.Lobby;

namespace StorageTest.Model
{
    public interface IGamePlayReadOnly
    {
        bool IsDisposed { get; }
        StartGameDTO StartGameDto { get; }
    }
}