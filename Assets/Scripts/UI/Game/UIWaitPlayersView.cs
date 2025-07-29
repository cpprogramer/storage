using Common;
using TMPro;
using UnityEngine;

namespace MonopolySpace.UI
{
    public sealed class UIWaitPlayersView : BaseView
    {
        [ SerializeField ] private TextMeshProUGUI _textCountPlayers;

        public void Setup( int countCurrent, int countMax ) => _textCountPlayers.text = $"{countCurrent}/{countMax}";

        protected override void Awake() => base.Awake();
    }
}