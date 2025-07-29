using System.Diagnostics;
using Assets.Scripts.Timeline;
using Common.Configs;
using UnityEngine;
using UnityEngine.Assertions;

namespace JigsawPuzzles.UI.View
{
    public abstract class BaseConsumableView : MonoBehaviour
    {
        [SerializeField] private BaseConsumableItemConfig _consumableItemConfig;
        public string Uid => _consumableItemConfig.Uid;
        
        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            Assert.IsNotNull(_consumableItemConfig, "_consumableItemConfig is null");
        }

        public abstract void UpdateConsumable(string itemKey, int itemValue, ITimeline timeline);
    }
}