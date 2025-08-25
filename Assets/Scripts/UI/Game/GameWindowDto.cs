using Common;
using Common.UI.Model;

namespace StorageTest.UI.ViewModel
{
    public class GameWindowDTO : BaseWindowDTO
    {
        public GameWindowDTO() : base( typeof(UIGameViewModel), Consts.UI_GAME_VIEW, WindowLayer.Windows, false ) {}
    }
}