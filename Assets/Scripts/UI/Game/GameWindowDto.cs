using Common.UI.Model;
using StorageTest;

namespace UI.Game
{
    public class GameWindowDTO : BaseWindowDTO
    {
        public GameWindowDTO() : base( typeof(UIGameViewModel), Consts.UI_GAME_VIEW ) {}
    }
}