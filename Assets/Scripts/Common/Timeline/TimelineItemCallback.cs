using System;

namespace Assets.Scripts.Timeline
{
    internal sealed class TimelineItemCallback : ITimelineItem
    {
        #region Properties

        public TimelineState State { get; private set; }

        #endregion

        #region Fields

        private readonly Action _action;

        #endregion

        #region Constructors

        public TimelineItemCallback( Action action )
        {
            State = TimelineState.IsPrepared;
            _action = action;
        }

        #endregion

        #region Methods

        public void Run()
        {
            State = TimelineState.IsRunning;
            _action();
            State = TimelineState.IsOver;
        }

        #endregion
    }
}