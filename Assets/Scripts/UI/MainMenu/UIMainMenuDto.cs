using Common.UI.Model;
using StorageTest;

namespace UI.MainMenu
{
    public sealed class UIMainMenuDTO : BaseWindowDTO
    {
        public UIMainMenuDTO() : base( typeof(UIMainMenuViewModel), Consts.UI_MAINMENU_VIEW ) {}
    }
}