using Common.Models;

namespace StorageTest.Messages
{
    public sealed class DummyDTO : IBaseDTO
    {
        public static DummyDTO Dummy = new();
        private DummyDTO() {}
    }
}