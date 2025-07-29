using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace MonopolySpace.Model
{
    public interface ILevelProvider : IDisposable
    {
        UniTask< ILevelView > GetLevelAsync( Transform parent );
    }
}