using System;

namespace Common
{
    public interface IBaseView
    {
        event Action OnClose;
        event Action OnStarted;
        WindowLayer WindowLayer { get; }
        bool IsRootView { get; }
        bool IsDestroyable { get; }
        void CloseView();
        void Hide();
        void Show();
    }
}