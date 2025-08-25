using Common.UI;
using Common.UI.Model;
using StorageTest.UI.View;

namespace StorageTest.UI.ViewModel
{
    public class UIHangarViewModel : UIBaseViewModel< UIHangarView >
    {
        public UIHangarViewModel( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator ) {}

        protected override void OnInitialize( BaseWindowDTO dto ) {}
    }
}