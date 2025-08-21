namespace StorageTest.Net
{
    public interface IPlayerIdProvider
    {
        string MasterClientId { get; }
        string GetMyPlayerId();
    }
}