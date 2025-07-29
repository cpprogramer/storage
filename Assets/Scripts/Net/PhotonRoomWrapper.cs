using Photon.Realtime;
using System.Collections;

namespace MonopolySpace.Net
{
    public sealed class PhotonRoomWrapper : IRoom
    {
        public string Id => room?.Name ?? string.Empty;

        public string Name => room?.Name ?? string.Empty;

        public string MasterClientId => ""; //PhotonNetwork.MasterClient.ActorNumber.ToString();

        public bool IsVisible
        {
            get => true;
            /*if ( PhotonNetwork.CurrentRoom == null )
                    return false;
                return PhotonNetwork.CurrentRoom.IsVisible;*/
            set
            {
                /* if ( PhotonNetwork.CurrentRoom != null )
                     PhotonNetwork.CurrentRoom.IsVisible = value;*/
            }
        }

        public int PlayerCount
        {
            get
            {
                if ( room == null )
                    return 0;

                /*if ( room != null && PhotonNetwork.CurrentRoom != null && room.Name == PhotonNetwork.CurrentRoom.Name )
                    return PhotonNetwork.CurrentRoom.PlayerCount;*/

                return room.PlayerCount;
            }
        }

        private readonly RoomInfo room;

        public PhotonRoomWrapper( RoomInfo room ) => this.room = room;

        public bool IsExistPlayer( string userId ) => false;

        public void ClearExpectedUsers()
        {
            //PhotonNetwork.CurrentRoom.ClearExpectedUsers();
        }

        // PhotonNetwork.PlayerList.FirstOrDefault( pl => pl.UserId == userId ) != null;
        public Hashtable GetRoomProperties()
        {
            if ( room == null ) return new Hashtable();
            return Helpers.FromPhotonHashTable( room.CustomProperties );
        }

        public void SetRoomProperties( Hashtable table )
        {
            //if ( Id == PhotonNetwork.CurrentRoom.Name )
            //    PhotonNetwork.CurrentRoom.SetCustomProperties( Helpers.FromSystemHashtable( table ) );
        }

        private Hashtable GetPlayersProperty( int id )
        {
            var result = new Hashtable();

            /*if ( Id == PhotonNetwork.CurrentRoom.Name )
            {
                Debug.Log( "Count players" + PhotonNetwork.PlayerList.Length );
                foreach ( Player pl in PhotonNetwork.PlayerList )
                    if ( pl.ActorNumber == id )
                        return Helpers.FromPhotonHashTable( pl.CustomProperties );
            }*/

            return result;
        }

        public void RemovePlayer( string playerId, bool isBot )
        {
            if ( !isBot ) KickPlayerById( playerId );
        }

        public void KickPlayerById( string playerId )
        {
            if ( !int.TryParse( playerId, out int id ) )
                return;

            /* if ( Id != PhotonNetwork.CurrentRoom.Name )
                 return;

             if ( !PhotonNetwork.IsMasterClient )
                 return;

             var idStr = id.ToString();

             Player player = PhotonNetwork.PlayerList.FirstOrDefault( pl => pl.ActorNumber == id );
             if ( player != null ) PhotonNetwork.CloseConnection( player );*/
        }
    }
}