using Common.Models;

namespace MonopolySpace.Messages
{
    public sealed class DummyModel : IBaseModel
    {
        private DummyModel() { }
        public static DummyModel Dummy = new DummyModel();
    }
}