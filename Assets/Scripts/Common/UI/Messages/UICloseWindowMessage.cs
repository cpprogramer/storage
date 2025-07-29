using System;

namespace Common.UI.Messages
{
    public sealed class UICloseWindowMessage
    {
        public Type WindowType { get; }
        public WindowResult WindowResult { get; }

        public UICloseWindowMessage( Type WindowType, WindowResult WindowResult )
        {
            this.WindowType = WindowType;
            this.WindowResult = WindowResult;
        }
    }
}