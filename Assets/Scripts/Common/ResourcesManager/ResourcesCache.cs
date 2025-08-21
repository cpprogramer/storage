using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Common
{
    public sealed class ResourcesCache : IResourcesCache
    {
        private readonly IResourcesProvider _resourcesProvider;
        private readonly List< string > _cache = new();

        public ResourcesCache( IResourcesProvider resourcesProvider ) => _resourcesProvider = resourcesProvider;

        public async UniTask< T > LoadResourceAsync< T >( string name )
            where T : Object
        {
            var result = await _resourcesProvider.LoadResourceAsync< T >( name );
            _cache.Add( name );
            return result;
        }

        public void Dispose()
        {
            _cache.ForEach( Addressables.Release );
            _cache.Clear();
        }
    }
}