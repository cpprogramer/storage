using MonopolySpace.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolySpace.UI
{
    public sealed class UIPlayerSlot : MonoBehaviour
    {
        [ SerializeField ] private TextMeshProUGUI _playerName;
        [ SerializeField ] private Image _imageActivePlayer;
        [ SerializeField ] private Image _imageProgressTurn;

        public void Setup( PlayerInfo player ) => _playerName.text = player.PlayerType.ToString();

        public void SetProgress( float normalizedVal ) => _imageProgressTurn.fillAmount = normalizedVal;

        public void MarkAsActive( bool isVisible ) => _imageActivePlayer.gameObject.SetActive( isVisible );
    }
}