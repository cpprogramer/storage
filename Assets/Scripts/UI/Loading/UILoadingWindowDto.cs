using Common.UI.Model;
using StorageTest;

namespace UI.Loading
{
    public sealed class UILoadingWindowDTO : BaseWindowDTO
    {
        public UILoadingWindowDTO() : base( typeof(UILoadingViewModel), Consts.UI_LOADING_VIEW ) {}
    }
}