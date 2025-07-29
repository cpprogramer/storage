using System;
using System.Collections.Generic;

namespace Common
{
    public interface IProcessingCommands
    {
        event EventHandler Completed;
        void AddCommand( ICommand cmd );
        void StartExecute();
    }

    public abstract class ProcessingCommands : IProcessingCommands
    {
        public event EventHandler Completed;
        public int Count => _queue.Count;

        private readonly Queue< ICommand > _queue = new();
        public virtual void StartExecute() => Execute();

        protected virtual void OnAllCommandsDone()
        {
            EventHandler handler = Completed;
            handler?.Invoke( this, new EventArgs() );
        }

        protected virtual void Execute() {}

        protected ICommand Dequeue()
        {
            if ( _queue.Count > 0 ) return _queue.Dequeue();

            return null;
        }

        public void AddCommand( ICommand cmd )
        {
            if ( cmd != null ) _queue.Enqueue( cmd );
        }

        public virtual void ForceFinish()
        {
            ICommand[] array = _queue.ToArray();
            _queue.Clear();
            array.ForEach( elt => elt.Dispose() );
        }
    }
}