using StorageTest.Model;

namespace StorageTest.Lobby
{
    public interface IGamePlayManager
    {
        IGamePlayReadOnly GamePlayReadOnly { get; }
    }
}