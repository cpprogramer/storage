using UnityEngine;

namespace Assets.Scripts.Timeline
{
    internal sealed class TimelineItemDelay : ITimelineItem
    {
        #region Properties

        TimelineState ITimelineItem.State
        {
            get
            {
                if ( _state != TimelineState.IsRunning ) return _state;

                if ( _startTime + _duration <= Time.time ) _state = TimelineState.IsOver;

                return _state;
            }
        }

        #endregion

        #region Constructors

        public TimelineItemDelay( float duration ) => _duration = duration;

        #endregion

        #region Methods

        void ITimelineItem.Run()
        {
            _startTime = Time.time;
            _state = TimelineState.IsRunning;
        }

        #endregion

        #region Fields

        private float _startTime;
        private TimelineState _state = TimelineState.IsPrepared;
        private readonly float _duration;

        #endregion
    }
}