using Cysharp.Threading.Tasks;
using System;
using Object = UnityEngine.Object;

namespace Common
{
    public interface IResourcesCache : IDisposable
    {
        public UniTask< T > LoadResourceAsync< T >( string name )
            where T : Object;
    }
}