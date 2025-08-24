using System;

namespace Common.UI
{
    public sealed class UIOpenedWindowMessage
    {
        public IUIViewModel< Type, WindowResult > IuiViewModel { get; }

        public UIOpenedWindowMessage( IUIViewModel< Type, WindowResult > iuiViewModel ) => IuiViewModel = iuiViewModel;
    }
}