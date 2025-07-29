using System;

namespace Common.UI.Model
{
    public abstract class BaseWindowModel : IWindowModel< Type >
    {
        public string WindowName { get; }
        public Type TypeWindow { get; }
        public int Id { get; }
        public bool IsModal { get; }
        public WindowLayer WindowLayer { get; }

        protected BaseWindowModel(
            Type typeWindow,
            string windowName,
            WindowLayer windowLayer = WindowLayer.Windows,
            int id = 0,
            bool isModal = false
        )
        {
            WindowName = windowName;
            TypeWindow = typeWindow;
            WindowLayer = windowLayer;
            Id = id;
            IsModal = isModal;
        }
    }
}