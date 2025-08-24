using Common.UI;
using Common.UI.Model;
using StorageTest.UI.View;

namespace StorageTest.UI.Controllers
{
    public class UILoadingController : UIBaseViewModel< UILoadingView >
    {
        public UILoadingController( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator )
        {
        }

        protected override void OnInitialize( BaseWindowDTO dto ) => _baseView.StartAnimation();
    }
}