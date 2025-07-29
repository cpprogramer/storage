using Common.UI.Model;
using System;

namespace Common.UI
{
    public interface IWindow< out TLogicAtom, TResult >
        where TLogicAtom : class
    {
        event Action< IWindow< TLogicAtom, TResult >, TResult > OnClosingFinished;
        
        TLogicAtom TypeWindow { get; }
        void Close( TResult result );
        void Initialize( BaseWindowModel msgModel );
    }
}