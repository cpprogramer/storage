using MonopolySpace.Net;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolySpace.UI
{
    public class UIRoomInfo : MonoBehaviour
    {
        public event Action OnJoinRoomClicked;
        [ SerializeField ] private TextMeshProUGUI _roomInfoText;
        [ SerializeField ] private Button _joinRoomButton;

        private IRoomInfo _room;
        private void Awake() => _joinRoomButton.onClick.AddListener( () => { OnJoinRoomClicked?.Invoke(); } );

        public void Setup( IRoomInfo room )
        {
            _room = room;
            _roomInfoText.text = room.Name;
        }
    }
}