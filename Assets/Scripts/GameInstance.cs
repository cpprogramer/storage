using Common;
using UnityEngine;

namespace StorageTest
{
    public interface IParentHolder
    {
        void Attach(Transform child);
    }

    public sealed class GameInstance : MonoBehaviour, IParentHolder
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Attach(Transform child)
        {
            child.SetParent(transform, false);
        }

        public void Setup(int instanceUid, bool debugPlayerMode)
        {
            var tickable = gameObject.AddComponent<Tickable>();
            var gameRoot = new GameRoot(instanceUid, tickable, this, debugPlayerMode);
            gameRoot.OnInitialized += OnInitialized;
            gameRoot.Create();
            gameRoot.Initialize();

            void OnInitialized()
            {
                gameRoot.OnInitialized -= OnInitialized;
                gameRoot.Run();
            }
        }
    }
}