namespace StorageTest.Net
{
    public sealed class PhotonRoomInfo : IRoomInfo
    {
        public string Name { get; }

        public PhotonRoomInfo( string itemName ) => Name = itemName;
    }
}