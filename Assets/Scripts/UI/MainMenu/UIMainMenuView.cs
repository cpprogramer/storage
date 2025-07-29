using Assets.Scripts.Timeline;
using Common;
using JigsawPuzzles.UI.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace MonopolySpace.UI.View
{
    public sealed class UIMainMenuView : BaseView
    {
        public event Action OnPlayClicked;
        public event Action OnGoldClicked; 

        [ SerializeField ] private Button _buttonPlay;
        [ SerializeField ] private Button _buttonGold;

        private readonly Dictionary<string, BaseConsumableView> _dictionaryConsumables =
            new Dictionary<string, BaseConsumableView>();
        
        protected override void Awake()
        {
            base.Awake();
            _buttonPlay.onClick.AddListener( () => OnPlayClicked?.Invoke() );
            
            var consumables = transform.GetComponentsInChildren<BaseConsumableView>(true);
            consumables.ForEach(item => { _dictionaryConsumables[item.Uid] = item; });
            _buttonGold.onClick.AddListener( () => OnGoldClicked?.Invoke() );
        }
        
        public void UpdateConsumables(IEnumerable<(string key, int value, int prevValue)> msgConsumableData)
        {
            msgConsumableData.ForEach(item =>
            {
                if (_dictionaryConsumables.TryGetValue(item.key, out var element))
                {
                    element.UpdateConsumable(item.key, item.value, _timeline);
                }
            });
        }

        [ Conditional( "UNITY_EDITOR" ) ]
        private void OnValidate() => Assert.IsNotNull( _buttonPlay, "_buttonPlay == null" );
    }
}