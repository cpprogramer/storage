using Common;
using Common.UI;
using Common.UI.Messages;
using Cysharp.Threading.Tasks;
using StorageTest.Net;
using StorageTest.UI.Controllers;
using System;
using UniRx;
using UnityEngine;

namespace FSM
{
    public sealed class NetLobbyFSMObject : BaseFiniteStateMachineObject
    {
        private const int WAIT_TIME_IN_SEC = 2;
        private readonly IMessageBroker _messageBroker;
        private readonly CompositeDisposable _disposable = new();
        private readonly IMultiplayerService _multiplayerService;

        public NetLobbyFSMObject(
            IFSM parentFsm,
            IMessageBroker messageBroker,
            IMultiplayerService multiplayerService
        ) : base( parentFsm )
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            _multiplayerService = multiplayerService ?? throw new ArgumentNullException( nameof(multiplayerService) );
        }

        protected override void OnStart()
        {
            _messageBroker.Publish( new UIOpenWindowMessage( new UINetLobbyModel() ) );
        }

        public override void Dispose()
        {
            _disposable.Dispose();
            _messageBroker.Publish( new UICloseWindowMessage( typeof(UINetLobbyController), WindowResult.Back ) );
        }

        public override void Update()
        {
            _messageBroker.Receive< UIExitFromLobbyToMainMenuMessage >()
                .Subscribe( UIExitFromLobbyToMainMenuMessageHandler ).AddTo( _disposable );

            _messageBroker.Receive< UIJoinOrCreateRoomMessage >().Subscribe( UIJoinOrCreateRoomMessageHandler )
                .AddTo( _disposable );
            
            _messageBroker.Receive< UIJoinRoomMessage >().Subscribe( UIJoinRoomMessageHandler )
                .AddTo( _disposable );
        }

        private void UIExitFromLobbyToMainMenuMessageHandler( UIExitFromLobbyToMainMenuMessage msg )
        {
            _multiplayerService.Disconnect();
            _parentFsm.SetState( typeof(MainMenuFSMObject) );
        }

        private void UIJoinOrCreateRoomMessageHandler( UIJoinOrCreateRoomMessage msg )
        {
            Debug.LogError( "[+] JOIN or Create" );
            TryJoinOrCreateRoom().Forget();
        }
        
        private void UIJoinRoomMessageHandler( UIJoinRoomMessage msg )
        {
            Debug.LogError( "[+] JOIN" );
            TryJoinRoom().Forget();
        }

        private async UniTaskVoid TryJoinOrCreateRoom()
        {
            Debug.LogError( "[+] JOIN 0" );
            await _multiplayerService.JoinOrCreateRoom();
            Debug.LogError( "[+] JOIN 1" );
        }
        
        private async UniTaskVoid TryJoinRoom()
        {
            Debug.LogError( "[+] JOIN 0" );
            await _multiplayerService.JoinOrCreateRoom();
            Debug.LogError( "[+] JOIN 1" );
        }
    }
}