using Photon.Client;
using System.Collections;

namespace MonopolySpace.Net
{
    public static class Helpers
    {
        public static Hashtable FromPhotonHashTable( PhotonHashtable table )
        {
            var result = new Hashtable();
            foreach ( object key in table.Keys ) result[ key ] = table[ key ];

            return result;
        }

        public static PhotonHashtable FromSystemHashtable( Hashtable table )
        {
            var result = new ExitGames.Client.Photon.Hashtable();
            foreach ( object key in table.Keys ) result[ key ] = table[ key ];

            return result;
        }
    }
}