namespace MonopolySpace.Net
{
    public class PhotonPlayerIdProvider : IPlayerIdProvider
    {
        public string MasterClientId => ""; //PhotonNetwork.MasterClient.ActorNumber.ToString();

        public string GetMyPlayerId() =>
            /*if ( PhotonNetwork.IsConnected )
                return PhotonNetwork.LocalPlayer.ActorNumber.ToString();*/
            default;
    }
}