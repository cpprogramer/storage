namespace Assets.Scripts.Timeline
{
    public interface ITimeline
    {
        bool IsPlaying { get; }
        float GetCurrentTime();
        float GetLastTweenEndTime();
        void Add( ITimelineData data );
        void Dispose();
    }
}