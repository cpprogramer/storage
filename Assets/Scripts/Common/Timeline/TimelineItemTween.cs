using DG.Tweening;

namespace Assets.Scripts.Timeline
{
    public sealed class TimelineItemTween : ITimelineItem
    {
        #region Properties

        public TimelineState State { get; private set; }

        #endregion

        #region Fields

        private readonly Tweener _tweener;

        #endregion

        #region Constructors

        public TimelineItemTween( Tweener tweener )
        {
            _tweener = tweener;
            _tweener.Pause();
        }

        #endregion

        #region Methods

        public void Run()
        {
            State = TimelineState.IsRunning;
            _tweener.OnComplete( () => State = TimelineState.IsOver );
            _tweener.Play();
        }

        #endregion
    }
}