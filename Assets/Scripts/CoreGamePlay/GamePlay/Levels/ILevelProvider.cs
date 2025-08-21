using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace StorageTest.Model
{
    public interface ILevelProvider : IDisposable
    {
        UniTask< ILevelView > GetLevelAsync( Transform parent );
    }
}