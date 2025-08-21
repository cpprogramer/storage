using System;
using System.Collections;
using System.Text;
using Random = UnityEngine.Random;

namespace StorageTest.Net
{
    internal class GuidRoomUniqueDataProvider : IRoomUniquDataProvider
    {
        string IRoomUniquDataProvider.GetUniqueId( IRoom room )
        {
            string name = room.Name;

            Hashtable parameters = room.GetRoomProperties();
            string[] partsArray = name.Split( '-' );
            if ( partsArray.Length < 3 ) return name;

            var result =
                $"{partsArray[ partsArray.Length - 3 ].Substring( 0, 3 )}{partsArray[ partsArray.Length - 2 ].Substring( 0, 3 )}";
            if ( parameters.ContainsKey( MatchmakingConst.ModeWithFriend ) &&
                 (bool)parameters[ MatchmakingConst.ModeWithFriend ] ) return "1" + result;

            return result;
        }

        string IRoomUniquDataProvider.GenerateRoomName( Hashtable parameters )
        {
            var name = Guid.NewGuid().ToString();
            /* if (parameters.ContainsKey(MatchmakingConst.MODE_WITH_BET) && true == (bool)parameters[MatchmakingConst.MODE_WITH_BET])
             {
                 return name;
             }*/

            string[] result = name.Split( '-' );
            for ( var i = 2; i < 4; ++i ) result[ i ] = ReplaceNumbers( result[ i ] );
            var sb = new StringBuilder();
            var index = 0;
            foreach ( string v in result )
            {
                ++index;
                sb.Append( v );
                if ( index < result.Length )
                    sb.Append( '-' );
            }

            if ( parameters.ContainsKey( MatchmakingConst.ModeWithFriend ) &&
                 (bool)parameters[ MatchmakingConst.ModeWithFriend ] ) return "+" + sb;

            return sb.ToString();
        }

        private static string ReplaceNumbers( string str )
        {
            for ( var i = 0; i < str.Length; ++i )
            {
                char chr = str[ i ];
                if ( char.IsLetter( chr ) )
                {
                    char rndNum = Random.Range( 0, 10 ).ToString()[ 0 ];
                    str = str.Replace( chr, rndNum );
                }
            }

            return str;
        }
    }
}