using Common.UI;
using Common.UI.Model;
using StorageTest.UI.View;

namespace StorageTest.UI.Controllers
{
    public class UILoadingController : Window< UILoadingView >
    {
        public UILoadingController( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator )
        {
        }

        protected override void OnInitialize( BaseWindowModel model ) => _baseView.StartAnimation();
    }
}