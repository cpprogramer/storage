using UnityEngine;

namespace Common.UI
{
    public interface IGUILayerHolder
    {
        Transform GetLayer( WindowLayer layer );
    }
}