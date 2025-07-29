using System;
using System.Collections.Generic;

namespace Common
{
    public interface IActionsQueue
    {
        void Add( Action action );
        void Clear();
        void Next();
        bool IsEmpty();
    }

    public class ActionsQueue : IActionsQueue
    {
        private readonly Stack< Action > _actionsQueue = new();
        public void Add( Action action ) => _actionsQueue.Push( action );

        public void Clear() => _actionsQueue.Clear();

        public bool IsEmpty() => _actionsQueue.Count == 0;

        public void Next()
        {
            if ( _actionsQueue.Count > 0 )
            {
                Action action = _actionsQueue.Pop();
                action();
            }
        }
    }
}