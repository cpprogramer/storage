using MonopolySpace.Lobby;
using MonopolySpace.Model;
using System.Collections.Generic;
using UnityEngine;

namespace MonopolySpace.UI
{
    public sealed class UIPlayerSlotsView : MonoBehaviour
    {
        [ SerializeField ] private UIPlayerSlot[] _slots;
        private UIPlayerSlot _currentActiveSlot;

        private readonly Dictionary< string, UIPlayerSlot > _dictionarySlots = new();
        private bool _isWaitTurn;
        private float _time;
        private float _maxTime;

        public void StopProgress() => _isWaitTurn = false;

        public void Setup( PlayerInfo[] players )
        {
            for ( var i = 0; i < _slots.Length; ++i )
            {
                PlayerInfo player = players[ i ];
                UIPlayerSlot slot = _slots[ i ];
                slot.Setup( player );
                _dictionarySlots.Add( player.Uid, slot );
            }
        }

        public void MarkPlayerAsActive( IPlayerReadOnly player )
        {
            if ( _currentActiveSlot != default ) _currentActiveSlot.MarkAsActive( false );

            if ( _dictionarySlots.TryGetValue( player.PlayerInfo.Uid, out UIPlayerSlot slot ) )
            {
                _currentActiveSlot = slot;
                _currentActiveSlot.MarkAsActive( true );
            }
        }

        private void Update()
        {
            if ( _isWaitTurn )
            {
                _time -= Time.deltaTime;
                float dt = _time / _maxTime;
                _currentActiveSlot.SetProgress( Mathf.Lerp( 0, 1, dt ) );
            }
        }

        public void StartProgress( float timerTime )
        {
            _maxTime = _time = timerTime;
            _currentActiveSlot.SetProgress( 1f );
            _isWaitTurn = true;
        }
    }
}