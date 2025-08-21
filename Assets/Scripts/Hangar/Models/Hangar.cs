using System;
using UnityEngine;

namespace StorageTest.Model
{
    public sealed class Hangar
    {
        public event Action OnHangarStarted;
        
        public void Initialize()
        {
            
        }

        public void Start()
        {
            OnHangarStarted?.Invoke();
        }
    }
}