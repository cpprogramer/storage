using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public class UILoadingWindowModel : BaseWindowModel
    {
        public UILoadingWindowModel() : base( typeof(UILoadingController), Consts.UI_LOADING_VIEW ) {}
    }
}