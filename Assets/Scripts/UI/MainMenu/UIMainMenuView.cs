using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common;
using JigsawPuzzles.UI.View;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace StorageTest.UI.View
{
    public sealed class UIMainMenuView : BaseView
    {
        [SerializeField] private Button _buttonPlay;
        public event Action OnPlayClicked;
        
        private readonly Dictionary<string, BaseConsumableView> _dictionaryConsumables = new();

        protected override void Awake()
        {
            base.Awake();
            _buttonPlay.onClick.AddListener(() => OnPlayClicked?.Invoke());

            var consumables = transform.GetComponentsInChildren<BaseConsumableView>(true);
            consumables.ForEach(item => { _dictionaryConsumables[item.Uid] = item; });
        }

        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            Assert.IsNotNull(_buttonPlay, "_buttonPlay == null");
        }
    }
}