using Common;
using JigsawPuzzles.UI.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public sealed class UIMainMenuView : BaseView
    {
        public event Action OnPlayClicked;
        [ SerializeField ] private Button _buttonPlay;

        private readonly Dictionary< string, BaseConsumableView > _dictionaryConsumables = new();

        protected override void Awake()
        {
            base.Awake();
            _buttonPlay.onClick.AddListener( () => OnPlayClicked?.Invoke() );

            BaseConsumableView[] consumables = transform.GetComponentsInChildren< BaseConsumableView >( true );
            consumables.ForEach( item => { _dictionaryConsumables[ item.Uid ] = item; } );
        }

        [ Conditional( "UNITY_EDITOR" ) ]
        private void OnValidate() => Assert.IsNotNull( _buttonPlay, "_buttonPlay == null" );
    }
}