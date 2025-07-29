using Assets.Scripts.Timeline;
using DG.Tweening;
using System;

namespace View.Timeline
{
    public static class TimelineExtensions
    {
        #region TimelineExtensions

        public static void Add( this ITimeline self, Action action, float startTime ) =>
            self.Add( new TimelineItemCallback( action ), startTime );

        public static void Add( this ITimeline self, Action action ) =>
            self.Add( new TimelineItemCallback( action ), self.GetLastTweenEndTime() );

        public static void Add< T >( this ITimeline self, Action< T > action, T param, float startTime ) =>
            self.Add( () => action( param ), startTime );

        public static void Add< T >( this ITimeline self, Action< T > action, T param ) =>
            self.Add( () => action( param ) );

        public static void Add( this ITimeline self, Tweener tweener, float startTime ) =>
            self.Add( new TimelineItemTween( tweener ), startTime, tweener.Duration() );

        public static void AddDelay( this ITimeline self, float duration ) =>
            self.Add( new TimelineItemDelay( duration ), self.GetLastTweenEndTime(), duration );

        public static void Add( this ITimeline self, ITimelineItem item, float time, float duration = 0f ) =>
            self.Add( new TimelineData( item, time, time + duration ) );

        #endregion
    }
}