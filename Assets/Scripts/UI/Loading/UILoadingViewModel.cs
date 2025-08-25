using Common.UI;
using Common.UI.Model;

namespace UI.Loading
{
    public class UILoadingViewModel : UIBaseViewModel< UILoadingView >
    {
        public UILoadingViewModel( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator ) {}

        protected override void OnInitialize( BaseWindowDTO dto ) {}
    }
}