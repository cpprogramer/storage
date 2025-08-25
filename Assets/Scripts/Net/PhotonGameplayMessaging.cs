using Photon.Client;
using System;

namespace StorageTest.Net
{
    public sealed class PhotonGameplayMessagingLegacy : IGameplayMessaging
    {
        public event Action< MessageData > OnEventReceived;

        public bool IsMasterClient => false; //PhotonNetwork.IsMasterClient;

        public bool IsLocalPlayer( string stringId ) => true;

        //int.TryParse( stringId, out int id ) && PhotonNetwork.LocalPlayer.ActorNumber == id;
        public void Dispose() => OnEventReceived = null;

        public void Enable() {} //PhotonNetwork.NetworkingClient.EventReceived += OnEvent;

        public void Disable() {} // PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;

        public void SendEvent( MessageCode code, object message, ReceiverGroupCustom receiverGroup )
        {
            var test = false;
            /*if ( PhotonNetwork.OfflineMode || test )
            {
                OnEvent( (byte)code, message, PhotonNetwork.LocalPlayer.ActorNumber );
                return;
            }*/

            /*PhotonNetwork.RaiseEvent( (byte)code, message,
                new RaiseEventOptions { Receivers = (ReceiverGroup)(int)receiverGroup },
                new SendOptions { Reliability = true, Encrypt = true } );*/
        }

        private void OnEvent( EventData data ) =>
            OnEventReceived?.Invoke(
                new MessageData( data.Sender.ToString(), (MessageCode)data.Code, data.CustomData ) );

        private void OnEvent( byte eventCode, object content, int senderId )
        {
            var data = new EventData();
            data.Parameters[ data.SenderKey ] = senderId;
            data.Parameters[ data.CustomDataKey ] = content;
            data.Code = eventCode;
            OnEvent( data );
        }
    }
}