using System.Collections;

namespace MonopolySpace.Net
{
    internal interface IRoomUniquDataProvider
    {
        string GenerateRoomName( Hashtable parameters );
        string GetUniqueId( IRoom room );
    }
}