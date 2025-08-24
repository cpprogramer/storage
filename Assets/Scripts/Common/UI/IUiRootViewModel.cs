using Cysharp.Threading.Tasks;
using System;
using CWindow = Common.UI.IUIViewModel< System.Type, Common.UI.WindowResult >;

namespace Common.UI
{
    public interface IUiRootViewModel
    {
        UniTask InitializeAsync();
    }
}