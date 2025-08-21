namespace StorageTest.Model
{
    public interface IGamePlay : IGamePlayReadOnly
    {
        void Dispose();
        void Start();
        void Initialize();
    }
}