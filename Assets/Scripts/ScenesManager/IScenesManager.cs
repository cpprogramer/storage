using System;
using UnityEngine.SceneManagement;

namespace Common
{
    public interface IScenesManager
    {
        event Action< Scene, LoadSceneMode > OnSceneLoaded;
        event Action< string > OnSceneUnLoaded;
        string CurrentSceneName { get; }
        Scene CurrentLoadedScene { get; }
        void AddScene( string name );
        void RemoveScene( string name );
    }
}