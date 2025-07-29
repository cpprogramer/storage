namespace Assets.Scripts.Timeline
{
    public interface ITimelineItem
    {
        TimelineState State { get; }

        void Run();
    }

    public enum TimelineState
    {
        IsPrepared,
        IsRunning,
        IsOver
    }
}