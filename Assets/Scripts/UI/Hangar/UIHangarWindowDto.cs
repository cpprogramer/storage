using Common.UI.Model;

namespace StorageTest.UI.ViewModel
{
    public sealed class UIHangarWindowDTO : BaseWindowDTO
    {
        public UIHangarWindowDTO() : base( typeof(UIHangarViewModel), Consts.UI_HAHAR_VIEW ) {}
    }
}