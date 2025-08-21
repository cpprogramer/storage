using StorageTest.Lobby;

namespace StorageTest.Model
{
    public interface IPlayerReadOnly
    {
        PlayerInfo PlayerInfo { get; }
    }
}