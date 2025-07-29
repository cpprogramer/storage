using Cysharp.Threading.Tasks;
using MonopolySpace.Model;
using System;

namespace MonopolySpace.View
{
    public interface IGameView : IDisposable
    {
        event Action OnLevelLoaded;
        ILevelView LevelRoot { get; }
        UniTask Start();
        UniTask Initialize();
    }
}