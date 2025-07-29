using Common;
using Common.UI.Model;
using MonopolySpace.Lobby;

namespace MonopolySpace.UI.Controllers
{
    public class GameWindowModel : BaseWindowModel
    {
        public IGamePlayManager GamePlayManager { get; }

        public GameWindowModel( IGamePlayManager gamePlayManager, WindowLayer windowLayer = WindowLayer.Windows, int id = 0, bool isModal = false ) : base(
            typeof(UIGameController), Consts.UIGAMEVIEW, windowLayer, id, isModal ) =>
            GamePlayManager = gamePlayManager;
    }
}