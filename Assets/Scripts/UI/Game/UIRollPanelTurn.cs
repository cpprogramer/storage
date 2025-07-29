using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolySpace.UI
{
    public class UIRollPanelTurn : MonoBehaviour
    {
        public event Action< RollDataMessage > OnRoll;
        [ SerializeField ] private Image _imageProgressWaitRoll;
        [ SerializeField ] private TMP_InputField _inputFieldNum1;
        [ SerializeField ] private TMP_InputField _inputFieldNum2;
        [ SerializeField ] private Button _rollButton;

        private bool _isWaitTurn;
        private float _time;
        private float _maxTime;

        public void Show( bool isVisible ) => gameObject.SetActive( isVisible );

        public void StopProgress() => _isWaitTurn = false;

        private void Awake() =>
            _rollButton.onClick.AddListener( () => { OnRoll?.Invoke( new RollDataMessage( 1, 1 ) ); } );

        private void SetProgressWaitRoll( float normalized ) => _imageProgressWaitRoll.fillAmount = normalized;

        public void StartProgress( float timerTime )
        {
            _maxTime = _time = timerTime;
            _imageProgressWaitRoll.fillAmount = 1f;
            _isWaitTurn = true;
        }

        private void Update()
        {
            if ( _isWaitTurn )
            {
                _time -= Time.deltaTime;
                float dt = _time / _maxTime;
                _imageProgressWaitRoll.fillAmount = Mathf.Lerp( 0, 1, dt );
            }
        }
    }
}