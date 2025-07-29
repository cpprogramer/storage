using MonopolySpace.Lobby;

namespace MonopolySpace.Model
{
    public interface IPlayerReadOnly
    {
        PlayerInfo PlayerInfo { get; }
    }
}