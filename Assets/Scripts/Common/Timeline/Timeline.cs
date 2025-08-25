using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Timeline
{
    public sealed class Timeline : ITimeline, IDisposable
    {
        public bool IsPlaying => _animationsList.Count > 0;

        private readonly List< ITimelineData > _animationsList = new();

        public float GetCurrentTime() => Time.time;

        public void Add( ITimelineData data ) => _animationsList.Add( data );

        public void Dispose() => _animationsList.Clear();

        public float GetLastTweenEndTime()
        {
            float furthestMoment = GetCurrentTime();
            foreach ( ITimelineData data in _animationsList )
            {
                float endTime = data.EndTime;
                if ( endTime > furthestMoment ) furthestMoment = endTime;
            }

            return furthestMoment;
        }

        //TODO Observable
        private void FixedUpdate()
        {
            float time = GetCurrentTime();
            for ( var i = 0; i < _animationsList.Count; ++i )
            {
                ITimelineData data = _animationsList[ i ];

                if ( data.Animation.State == TimelineState.IsPrepared && time >= data.StartTime ) data.Animation.Run();
            }

            _animationsList.RemoveAll( element => element.Animation.State == TimelineState.IsOver );
        }
    }
}