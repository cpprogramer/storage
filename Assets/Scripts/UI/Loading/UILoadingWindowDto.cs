using Common.UI.Model;

namespace StorageTest.UI.ViewModel
{
    public sealed class UILoadingWindowDTO : BaseWindowDTO
    {
        public UILoadingWindowDTO() : base( typeof(UILoadingViewModel), Consts.UI_LOADING_VIEW ) {}
    }
}