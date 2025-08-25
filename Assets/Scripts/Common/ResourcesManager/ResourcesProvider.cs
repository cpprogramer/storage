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
        private readonly Dictionary< string, List< AsyncOperationHandle > > _dicLoadedResources = new();

        //для того чтобы не было случая "конкурентного" обращения к словарю
        private readonly Stack< string > _stackToRelease = new();

        public async UniTask< T > LoadResourceAsync< T >( string name )
            where T : Object
        {
            try
            {
                while ( _stackToRelease.Count > 0 ) ReleaseForce( _stackToRelease.Pop() );

                DeleteAllInvalidHandles();
                return await LoadAndAddHandleToDictionaryAsync();
            }
            catch ( Exception e )
            {
                Debug.LogError( $"[+] Error load name:{name} message:{e.Message}" );
            }

            return null;

            async UniTask< T > LoadAndAddHandleToDictionaryAsync()
            {
                bool isExist = _dicLoadedResources.ContainsKey( name );
                if ( !isExist ) _dicLoadedResources[ name ] = new List< AsyncOperationHandle >();
                AsyncOperationHandle< T > asyncOperation = Addressables.LoadAssetAsync< T >( name );
                _dicLoadedResources[ name ].Add( asyncOperation );
                return await asyncOperation.ToUniTask();
            }
        }

        public void Release( string name, bool force = false )
        {
            if ( force )
                ReleaseForce( name );
            else
                _stackToRelease.Push( name );
        }

        private void ReleaseForce( string name )
        {
            if ( _dicLoadedResources.TryGetValue( name, out List< AsyncOperationHandle > list ) )
                for ( int i = list.Count - 1; i >= 0; --i )
                {
                    AsyncOperationHandle handle = list[ i ];
                    list.RemoveAt( i );
                    if ( handle.IsValid() )
                    {
                        Addressables.Release( handle );
                        break;
                    }
                }
        }

        private void DeleteAllInvalidHandles() =>
            _dicLoadedResources.ForEach( kv =>
            {
                for ( int i = kv.Value.Count - 1; i >= 0; --i )
                {
                    AsyncOperationHandle handle = kv.Value[ i ];
                    if ( !handle.IsValid() ) kv.Value.RemoveAt( i );
                }
            } );
    }
}