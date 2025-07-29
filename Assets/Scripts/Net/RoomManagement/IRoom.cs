using System.Collections;

namespace MonopolySpace.Net
{
    public interface IRoom
    {
        //event EventHandler<EventArgsGeneric<string>> PlayerAdded;
        //event EventHandler<EventArgsGeneric<string>> PlayerRemoved;

        string Id { get; }
        string Name { get; }
        string MasterClientId { get; }
        bool IsVisible { get; set; }
        int PlayerCount { get; }
        Hashtable GetRoomProperties();

        void SetRoomProperties( Hashtable table );

        //void AddBotPlayer(string playerID, IRoomPlayerInfo properties);
        void RemovePlayer( string playerID, bool isBot );
        void KickPlayerById( string id );
        bool IsExistPlayer( string userId );
        void ClearExpectedUsers();
    }
}