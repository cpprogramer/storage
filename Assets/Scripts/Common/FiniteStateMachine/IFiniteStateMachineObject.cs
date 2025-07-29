using System;

namespace Common
{
    public interface IFiniteStateMachineObject : IDisposable
    {
        void Enter();
        void Exit();
        void Update();
    }
}