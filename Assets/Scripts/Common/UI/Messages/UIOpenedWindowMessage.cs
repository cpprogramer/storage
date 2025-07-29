using System;

namespace Common.UI
{
    public sealed class UIOpenedWindowMessage
    {
        public IWindow< Type, WindowResult > Window { get; }

        public UIOpenedWindowMessage( IWindow< Type, WindowResult > window ) => Window = window;
    }
}