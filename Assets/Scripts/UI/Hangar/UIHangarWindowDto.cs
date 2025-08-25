using Common.UI.Model;
using UI.Hangar;

namespace StorageTest.UI.ViewModel
{
    public sealed class UIHangarWindowDTO : BaseWindowDTO
    {
        public UIHangarWindowDTO() : base( typeof(UIHangarViewModel), Consts.UI_HAHAR_VIEW ) {}
    }
}