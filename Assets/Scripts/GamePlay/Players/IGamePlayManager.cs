using MonopolySpace.Model;

namespace MonopolySpace.Lobby
{
    public interface IGamePlayManager
    {
        IGamePlayReadOnly GamePlayReadOnly { get; }
        IWaitingPlayerMakeDecisionHandler WaitingPlayerMakeDecisionHandler { get; }
        IWaitingPlayerMakeTurnHandler WaitingPlayerMakeTurnHandler { get; }
    }
}