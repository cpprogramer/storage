using Common.UI.Model;

namespace StorageTest.UI.ViewModel
{
    public sealed class UINetLobbyDTO : BaseWindowDTO
    {
        public UINetLobbyDTO() : base( typeof(UINetLobbyViewModel), Consts.UI_NET_LOBBY_VIEW ) {}
    }
}