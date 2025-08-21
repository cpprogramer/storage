using System.Collections;

namespace StorageTest.Net
{
    internal interface IRoomUniquDataProvider
    {
        string GenerateRoomName( Hashtable parameters );
        string GetUniqueId( IRoom room );
    }
}