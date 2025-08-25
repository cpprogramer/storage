using Common;
using StorageTest.Net;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace StorageTest.UI.View
{
    public sealed class UINetLobbyView : BaseView
    {
        public event Action OnExitClicked;
        public event Action OnJoinOrCreateRoomClicked;
        public event Action OnTest;
        [ SerializeField ] private bool _isDebug;

        [ SerializeField ] private Button _buttonExit;
        [ SerializeField ] private Button _buttonJoinOrCreate;
        [ SerializeField ] private UIRoomListView _roomListView;

        public bool IsDebug => _isDebug;

        public void Setup( IRoomInfo[] rooms ) => _roomListView.Setup( rooms );

        protected override void Awake()
        {
            base.Awake();
            _buttonExit.onClick.AddListener( () => OnExitClicked?.Invoke() );
            _buttonJoinOrCreate.onClick.AddListener( () => OnJoinOrCreateRoomClicked?.Invoke() );
        }

        private void Update()
        {
            if ( _isDebug )
            {
                _isDebug = false;
                OnTest?.Invoke();
            }
        }

        [ Conditional( "UNITY_EDITOR" ) ]
        private void OnValidate()
        {
            Assert.IsNotNull( _buttonExit, "_buttonExit == null" );
            Assert.IsNotNull( _roomListView, "_roomListView == null" );
        }
    }
}