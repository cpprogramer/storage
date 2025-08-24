using Common;
using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public class GameWindowDTO : BaseWindowDTO
    {
        public GameWindowDTO() : base( typeof(UIGameController), Consts.UI_GAME_VIEW, WindowLayer.Windows, false ) {}
    }
}