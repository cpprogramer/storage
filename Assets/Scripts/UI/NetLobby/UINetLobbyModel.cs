using Common.UI.Model;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UINetLobbyModel : BaseWindowModel
    {
        public UINetLobbyModel() : base( typeof(UINetLobbyController), Consts.UINETLOBBYVIEW ) {}
    }
}