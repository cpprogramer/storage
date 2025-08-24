using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public sealed class UIHangarWindowDTO : BaseWindowDTO
    {
        public UIHangarWindowDTO() : base( typeof(UIHangarController), Consts.UI_HAHAR_VIEW ) {}
    }
}