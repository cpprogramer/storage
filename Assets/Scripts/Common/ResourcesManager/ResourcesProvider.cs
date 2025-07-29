using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Common
{
    public sealed class ResourcesProvider : IResourcesProvider
    {
        private readonly Dictionary< string, AsyncOperationHandle > _dicLoadedResources = new();

        public async UniTask< T > LoadResourceAsync< T >( string name )
            where T : Object
        {
            try
            {
                var dependenciesAsync = Addressables.DownloadDependenciesAsync( name, true );
                await dependenciesAsync;
                
                AsyncOperationHandle< T > asyncOperation = Addressables.LoadAssetAsync< T >( name );
                _dicLoadedResources.Add( name, asyncOperation );
                return await asyncOperation;
            }
            catch ( Exception e )
            {
                Debug.LogError( $"Error load name:{name} message:{e.Message}" );
            }

            return default;
        }

        public void Release( string name )
        {
            if ( _dicLoadedResources.Remove( name, out AsyncOperationHandle result ) )
            {
                Addressables.Release( result );
            }
        }

        public void Dispose()
        {
            _dicLoadedResources.ForEach( item=> Addressables.Release( item.Value ));
            _dicLoadedResources.Clear();
        }
    }
}