using Common;
using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public sealed class UIMainMenuModel : BaseWindowModel
    {
        public UIMainMenuModel() : base( typeof(UIMainMenuController), Consts.UI_MAINMENU_VIEW, WindowLayer.Windows,  0, true ) {}
    }
}