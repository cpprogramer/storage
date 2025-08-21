using Common.Models;

namespace StorageTest.Messages
{
    public sealed class DummyModel : IBaseModel
    {
        private DummyModel() { }
        public static DummyModel Dummy = new DummyModel();
    }
}