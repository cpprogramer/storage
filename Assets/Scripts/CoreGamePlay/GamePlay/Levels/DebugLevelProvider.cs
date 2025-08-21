using Common;
using Cysharp.Threading.Tasks;
using StorageTest.GamePlay;
using UnityEngine;

namespace StorageTest.Model
{
    public sealed class DebugLevelProvider : ILevelProvider
    {
        private const string LEVEL_NAME = "LevelView";
        private readonly IResourcesProvider _resourcesProvider;

        public DebugLevelProvider(IResourcesProvider resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;
        }

        public void Dispose()
        {
            _resourcesProvider.Release(LEVEL_NAME);
        }

        public async UniTask<ILevelView> GetLevelAsync(Transform parent)
        {
            var level = await _resourcesProvider.LoadResourceAsync<GameObject>(LEVEL_NAME);
            var levelInst = Object.Instantiate(level);
            var levelView = levelInst.GetComponent<LevelView>();
            levelView.Setup(_resourcesProvider, parent);
            //await levelView.CreateView();

            return levelView;
        }
    }
}