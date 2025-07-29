using System;
using UnityEngine.SceneManagement;

namespace Common
{
    public class ScenesManager : IScenesManager
    {
        public event Action< Scene, LoadSceneMode > OnSceneLoaded;
        public event Action< string > OnSceneUnLoaded;

        public string CurrentSceneName { get; private set; } = string.Empty;
        public Scene CurrentLoadedScene { get; private set; }
        //⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊ 

        private readonly Scene _rootScene;

        //⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊
        public ScenesManager()
        {
            SceneManager.sceneLoaded += SceneLoadedHandler;
            SceneManager.sceneUnloaded += SceneUnLoadedHandler;
            _rootScene = SceneManager.GetActiveScene();
        }

        //⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊
        public void AddScene( string name ) => SceneManager.LoadSceneAsync( name, LoadSceneMode.Additive );

        //⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊ 
        private void SceneLoadedHandler( Scene scene, LoadSceneMode mode )
        {
            CurrentLoadedScene = scene;
            CurrentSceneName = scene.name;
            SceneManager.SetActiveScene( scene );
            OnSceneLoaded?.Invoke( scene, mode );
        }

        //⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊ 
        private void SceneUnLoadedHandler( Scene scene )
        {
            SceneManager.SetActiveScene( _rootScene );
            OnSceneUnLoaded?.Invoke( scene.name );
            CurrentSceneName = string.Empty;
            CurrentLoadedScene = default;
        }

        //⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊⚊
        public void RemoveScene( string name )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                if ( !string.IsNullOrEmpty( CurrentSceneName ) )
                    SceneManager.UnloadSceneAsync( CurrentSceneName );
                else
                    OnSceneUnLoaded?.Invoke( name );
            }
            else
            {
                if ( !string.IsNullOrEmpty( name ) ) SceneManager.UnloadSceneAsync( name );
            }
        }
    }
}