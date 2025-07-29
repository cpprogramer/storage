using MonopolySpace.Lobby;

namespace MonopolySpace.Model
{
    public interface IGamePlayReadOnly
    {
        bool IsDisposed { get; }
        StartGameModel StartGameModel { get; }
    }
}