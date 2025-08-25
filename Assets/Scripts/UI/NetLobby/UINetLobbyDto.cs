using Common.UI.Model;
using StorageTest;

namespace UI.NetLobby
{
    public sealed class UINetLobbyDTO : BaseWindowDTO
    {
        public UINetLobbyDTO() : base( typeof(UINetLobbyViewModel), Consts.UI_NET_LOBBY_VIEW ) {}
    }
}