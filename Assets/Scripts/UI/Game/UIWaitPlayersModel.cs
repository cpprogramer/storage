using Common;
using Common.UI.Model;

namespace MonopolySpace.UI.Controllers
{
    public class UIWaitPlayersModel : BaseWindowModel
    {
        public UIWaitPlayersModel( WindowLayer windowLayer = WindowLayer.Windows,  int id = 0, bool isModal = false ) : base( typeof(UIWaitPlayersController),
            Consts.UIWAITPLAYERSVIEW, windowLayer, id, isModal )
        {
        }
    }
}