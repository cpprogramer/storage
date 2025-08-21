using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Common
{
    public interface IScenesManager
    {
        event Action< Scene, LoadSceneMode > OnSceneLoaded;
        event Action< string > OnSceneUnLoaded;
        string CurrentSceneName { get; }
        Scene CurrentLoadedScene { get; }
        UniTask< Scene > AddSceneAsync( string name, CancellationToken cancellationToken = default );
        void RemoveScene( string name );
    }
}