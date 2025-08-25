using System;

namespace Hangar.Models
{
    public sealed class Hangar
    {
        public event Action OnHangarStarted;

        public void Start() => OnHangarStarted?.Invoke();

        public void Initialize() {}
    }
}