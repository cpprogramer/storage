using Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StorageTest
{
    public sealed class GameInstance : MonoBehaviour, IParentHolder
    {
        private GameRoot _gameRoot;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Attach(Transform child)
        {
            child.SetParent(transform, false);
        }

        public async UniTask InitializeAsync(int instanceUid, bool debugPlayerMode)
        {
            var tickable = gameObject.AddComponent<Tickable>();
            _gameRoot = new GameRoot(instanceUid, tickable, this, debugPlayerMode);
            _gameRoot.Create();
            await _gameRoot.InitializeAsync();
            //gameRoot.OnInitialized += OnInitialized;
            //gameRoot.Create();
            //gameRoot.Initialize();

            void OnInitialized()
            {
                //gameRoot.OnInitialized -= OnInitialized;
                //gameRoot.Run();
            }
        }

        public void Run()
        {
            _gameRoot.Run();
        }
    }
}