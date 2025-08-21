using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
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
        
        public async UniTask< Scene > AddSceneAsync( string sceneName, CancellationToken cancellationToken = default )
        {
            // Start loading the scene asynchronously with LoadSceneMode.Additive
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync( sceneName, LoadSceneMode.Additive );

            // Wait until the scene is fully loaded
            await UniTask.WaitUntil( () => asyncOperation.isDone, cancellationToken: cancellationToken );

            // Retrieve the loaded scene
            Scene loadedScene = SceneManager.GetSceneByName( sceneName );
            if ( !loadedScene.IsValid() )
            {
                Debug.LogError( $"Scene {sceneName} failed to load or is invalid." );
                return default; // Return default if the scene is invalid
            }

            Debug.Log( $"Scene {sceneName} loaded successfully!" );
            return loadedScene;
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
    }
}