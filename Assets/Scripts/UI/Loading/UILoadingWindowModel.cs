using Common.UI.Model;

namespace MonopolySpace.UI.Controllers
{
    public class UILoadingWindowModel : BaseWindowModel
    {
        public UILoadingWindowModel() : base( typeof(UILoadingController), Consts.UILOADINGVIEW ) {}
    }
}