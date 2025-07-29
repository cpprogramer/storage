using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolySpace.UI
{
    public class UIRollPanelDecision : MonoBehaviour
    {
        public event Action< RollDataMessage > OnRoll;
        [ SerializeField ] private Image _imageProgressWaitRoll;
        [ SerializeField ] private TMP_InputField _inputFieldNum1;
        [ SerializeField ] private TMP_InputField _inputFieldNum2;
        [ SerializeField ] private Button _rollButton;

        public void Show( bool isVisible ) => gameObject.SetActive( isVisible );

        private void Awake() =>
            _rollButton.onClick.AddListener( () => { OnRoll?.Invoke( new RollDataMessage( 1, 1 ) ); } );

        private void SetProgressWaitRoll( float normalized ) => _imageProgressWaitRoll.fillAmount = normalized;
    }
}