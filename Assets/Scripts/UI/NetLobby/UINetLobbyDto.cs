using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public sealed class UINetLobbyDTO : BaseWindowDTO
    {
        public UINetLobbyDTO() : base( typeof(UINetLobbyController), Consts.UI_NET_LOBBY_VIEW ) {}
    }
}