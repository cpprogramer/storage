using Cysharp.Threading.Tasks;

namespace Common.UI
{
    public interface IUiRootViewModel
    {
        UniTask InitializeAsync();
    }
}