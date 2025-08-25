using Cysharp.Threading.Tasks;
using StorageTest.Model;
using System;

namespace StorageTest.View
{
    public interface IGameView : IDisposable
    {
        event Action OnLevelLoaded;
        ILevelView LevelRoot { get; }
        UniTask Start();
        UniTask Initialize();
    }
}