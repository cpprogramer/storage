using Cysharp.Threading.Tasks;
using System;
using StorageTest.Model;

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