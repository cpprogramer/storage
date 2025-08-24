using Common.UI.Model;

namespace StorageTest.UI.Controllers
{
    public sealed class UILoadingWindowDTO : BaseWindowDTO
    {
        public UILoadingWindowDTO() : base( typeof(UILoadingController), Consts.UI_LOADING_VIEW ) {}
    }
}