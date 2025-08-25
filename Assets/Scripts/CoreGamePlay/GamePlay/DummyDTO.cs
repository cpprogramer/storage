using Common.Models;

namespace StorageTest.Messages
{
    public sealed class DummyDTO : IBaseDTO
    {
        private DummyDTO() { }
        public static DummyDTO Dummy = new DummyDTO();
    }
}