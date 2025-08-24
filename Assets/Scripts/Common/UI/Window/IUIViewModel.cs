using Common.UI.Model;
using System;

namespace Common.UI
{
    public interface IUIViewModel< out TLogicAtom, TResult >
        where TLogicAtom : class
    {
        event Action< IUIViewModel< TLogicAtom, TResult >, TResult > OnClosingFinished;
        
        TLogicAtom TypeWindow { get; }
        void Close( TResult result );
        void Initialize( BaseWindowDTO msgDto );
    }
}