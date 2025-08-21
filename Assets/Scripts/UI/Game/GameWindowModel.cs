using Common;
using Common.UI.Model;
using StorageTest.Lobby;

namespace StorageTest.UI.Controllers
{
    public class GameWindowModel : BaseWindowModel
    {
        public GameWindowModel(IGamePlayManager gamePlayManager, WindowLayer windowLayer = WindowLayer.Windows,
            int id = 0, bool isModal = false) : base(
            typeof(UIGameController), Consts.UI_GAME_VIEW, windowLayer, id, isModal)
        {
            GamePlayManager = gamePlayManager;
        }

        public IGamePlayManager GamePlayManager { get; }
    }
}