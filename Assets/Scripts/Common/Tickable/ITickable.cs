using System;

namespace Common
{
    public interface ITickable
    {
        event Action OnFixedTick;
        event Action OnTick;
    }
}