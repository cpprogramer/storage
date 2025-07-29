using System;

namespace Common
{
    public abstract class Command : ICommand
    {
        public event Action OnCompleted;
        public string CommandKey
        {
            get => GetType().Name;
            set {}
        }

        protected virtual void Complete()
        {
            Dispose();
            OnCompleted?.Invoke();
        }

        public abstract void Do();

        public virtual void Dispose() {}
    }
}