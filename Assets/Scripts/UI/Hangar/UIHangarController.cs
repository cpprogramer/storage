using Common.UI;
using Common.UI.Model;
using StorageTest.UI.View;

namespace StorageTest.UI.Controllers
{
    public class UIHangarController : UIBaseViewModel< UIHangarView >
    {
        public UIHangarController( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator ) {}

        protected override void OnInitialize( BaseWindowDTO dto ) {}
    }
}