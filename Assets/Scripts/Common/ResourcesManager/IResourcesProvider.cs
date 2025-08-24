using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Common
{
    public interface IResourcesProvider
    {
        UniTask< T > LoadResourceAsync< T >( string name )
            where T : Object;

        public void Release( string name, bool force = false );
    }
}