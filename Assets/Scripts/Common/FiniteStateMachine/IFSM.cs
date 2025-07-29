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

        void RegisterState( Type state, Func< IBaseModel, IFiniteStateMachineObject > func );
        void SetState( Type state, IBaseModel model = null );
        EReasonFSM CanSetState( Type state, IBaseModel model = null );
    }
}