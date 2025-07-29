using System;

namespace Common
{
    public interface ICommand : IDisposable
    {
        event Action OnCompleted;
        void Do();
    }
}