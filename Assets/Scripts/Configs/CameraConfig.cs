using System.Linq;
using UnityEngine;

namespace Configs
{
    public sealed class CameraConfig : ScriptableObject
    {
        [ SerializeField ] private CameraData[] _data;

        public CameraData GetDataByUID( int uid ) => _data.FirstOrDefault( item => item.instanceId == uid );
    }
}