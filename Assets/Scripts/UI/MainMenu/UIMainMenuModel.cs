using Common;
using Common.UI.Model;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UIMainMenuModel : BaseWindowModel
    {
        public UIMainMenuModel() : base( typeof(UIMainMenuController), Consts.UIMAINMENUVIEW, WindowLayer.Windows,  0, true ) {}
    }
}