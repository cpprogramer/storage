using System;

namespace Common.UI.Model
{
    public abstract class BaseWindowDTO : IWindowDTO< Type >
    {
        public string WindowName { get; }
        public Type TypeWindow { get; }
        public bool IsModal { get; }
        public WindowLayer WindowLayer { get; }

        protected BaseWindowDTO(
            Type typeWindow,
            string windowName,
            WindowLayer windowLayer = WindowLayer.Windows,
            bool isModal = false
        )
        {
            WindowName = windowName;
            TypeWindow = typeWindow;
            WindowLayer = windowLayer;
            IsModal = isModal;
        }
    }
}