using Common.Models;
using System;

namespace Common
{
    public enum EReasonFSM
    {
        Undefined,
        Success,
        Equal
    }

    public interface IFSM : IDisposable
    {
        event Action OnChanged;
        event Action OnChanging;

        void RegisterState( Type state, Func< IBaseDTO, IFiniteStateMachineObject > func );
        void SetState( Type state, IBaseDTO dto = null );
        EReasonFSM CanSetState( Type state, IBaseDTO dto = null );
    }
}