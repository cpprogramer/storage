using Common;
using System;
using UnityEngine;

namespace MonopolySpace.UI
{
    public sealed class UIGameView : BaseView
    {
        public event Action< RollDataMessage > OnRoll;

        [ SerializeField ] private UIRollPanelTurn _uiRollPanelTurn;
        [ SerializeField ] private UIRollPanelDecision _uiRollPanelDecision;
        [ SerializeField ] private UIPlayerSlotsView _playerSlotsView;

        public UIPlayerSlotsView PlayerSlotsView => _playerSlotsView;
        public UIRollPanelTurn RollPanelTurn => _uiRollPanelTurn;
        public UIRollPanelDecision RollPanelDecision => _uiRollPanelDecision;

        protected override void Awake()
        {
            base.Awake();
            _uiRollPanelTurn.OnRoll += data => OnRoll?.Invoke( data );
        }
    }
}