using System;

namespace StorageTest.Model
{
    public sealed class Hangar
    {
        public event Action OnHangarStarted;

        public void Initialize() {}

        public void Start() => OnHangarStarted?.Invoke();
    }
}