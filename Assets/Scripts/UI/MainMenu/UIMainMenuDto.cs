using Common.UI.Model;

namespace StorageTest.UI.ViewModel
{
    public sealed class UIMainMenuDTO : BaseWindowDTO
    {
        public UIMainMenuDTO() : base( typeof(UIMainMenuViewModel), Consts.UI_MAINMENU_VIEW ) {}
    }
}