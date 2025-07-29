using Common.UI;
using Common.UI.Model;
using MonopolySpace.UI.View;

namespace MonopolySpace.UI.Controllers
{
    public class UILoadingController : Window< UILoadingView >
    {
        public UILoadingController( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator )
        {
        }

        protected override void OnInitialize( BaseWindowModel model ) => _baseView.StartAnimation();
    }
}