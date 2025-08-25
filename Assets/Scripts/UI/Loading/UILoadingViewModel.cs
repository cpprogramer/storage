using Common.UI;
using Common.UI.Model;
using StorageTest.UI.View;

namespace StorageTest.UI.ViewModel
{
    public class UILoadingViewModel : UIBaseViewModel< UILoadingView >
    {
        public UILoadingViewModel( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator ) {}

        protected override void OnInitialize( BaseWindowDTO dto ) {}
    }
}