using System;
using UnityEngine;

namespace Common
{
    public sealed class Tickable : MonoBehaviour, ITickable
    {
        public event Action OnFixedTick;
        public event Action OnTick;

        private void Update() => OnTick?.Invoke();

        private void FixedUpdate() => OnFixedTick?.Invoke();
    }
}