using System;
using CWindow = Common.UI.IWindow< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public interface IUiRootViewModel
    {
        event Action OnInitialized;

        void RegisterWindow( Type type, Func<CWindow> wnd );

        void ReleaseView( string name );
        void Initialize();
    }
}