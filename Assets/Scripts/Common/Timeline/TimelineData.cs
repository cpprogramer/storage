using Assets.Scripts.Timeline;

namespace View.Timeline
{
    internal sealed class TimelineData : ITimelineData
    {
        #region Constructors

        public TimelineData( ITimelineItem animation, float startTime, float endTime )
        {
            Animation = animation;

            StartTime = startTime;
            EndTime = endTime;
        }

        #endregion Constructors

        #region Properties

        public float StartTime { get; }

        public float EndTime { get; }

        public ITimelineItem Animation { get; }

        #endregion Properties
    }
}