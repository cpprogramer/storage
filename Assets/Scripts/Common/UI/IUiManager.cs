using System;

namespace Common.UI
{
    public interface IUiManager< in TLogicAtom, TResult >
        where TLogicAtom : class
    {
        void RegisterWindow( TLogicAtom type,  Func<IWindow< TLogicAtom, TResult >> func );
    }
}