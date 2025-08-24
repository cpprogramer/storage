using Common;
using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public sealed class UIMainMenuDTO : BaseWindowDTO
    {
        public UIMainMenuDTO() : base( typeof(UIMainMenuController), Consts.UI_MAINMENU_VIEW, WindowLayer.Windows,  false ) {}
    }
}