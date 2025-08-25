using Common.UI;
using Common.UI.Model;

namespace UI.Hangar
{
    public class UIHangarViewModel : UIBaseViewModel< UIHangarView >
    {
        public UIHangarViewModel( IUiRootAggregator uiRootAggregator ) : base( uiRootAggregator ) {}

        protected override void OnInitialize( BaseWindowDTO dto ) {}
    }
}