namespace Assets.Scripts.Timeline
{
    public interface ITimelineData
    {
        float StartTime { get; }
        float EndTime { get; }
        ITimelineItem Animation { get; }
    }
}