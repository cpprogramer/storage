namespace Configs
{
    public interface IGamePlayConfig
    {
        int WaitDecisionTimeInSec { get; }
        int WaitMakeTurnTimeInSec { get; }
    }
}