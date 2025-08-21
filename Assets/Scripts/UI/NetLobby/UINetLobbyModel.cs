using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public sealed class UINetLobbyModel : BaseWindowModel
    {
        public UINetLobbyModel() : base( typeof(UINetLobbyController), Consts.UI_NET_LOBBY_VIEW ) {}
    }
}