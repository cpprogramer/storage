using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFSM
{
    public class GameFsm : IFiniteStateMachine< IFiniteStateMachineObject >, IFSM
    {
        public event Action OnChanged;
        public event Action OnChanging;

        public IFiniteStateMachineObject CurrentState { get; private set; }
     
        private readonly Dictionary< Type, Func< IBaseDTO, IFiniteStateMachineObject > > _stateMachines = new();

        public EReasonFSM CanSetState( Type state, IBaseDTO dto = null )
        {
            if ( CurrentState != null && state == CurrentState.GetType() )
            {
                return EReasonFSM.Equal;
            }
            
            return EReasonFSM.Success;
        }

        public void RegisterState( Type type, Func< IBaseDTO, IFiniteStateMachineObject > func )
        {
            if ( !_stateMachines.TryAdd( type, func ) )
            {
                Debug.LogError( "Error : Can't add state to GameFSM" );
                throw new Exception( "Can't add state to GameFSM" );
            }
        }

        void IFSM.SetState( Type state, IBaseDTO dto = null )
        {
            if ( CanSetState( state, dto ) != EReasonFSM.Success ) return;

            if ( _stateMachines.TryGetValue( state, out Func< IBaseDTO, IFiniteStateMachineObject > result ) )
            {
                OnChanging?.Invoke();
                CurrentState?.Exit();
                CurrentState?.Dispose();

                CurrentState = result.Invoke( dto );
                CurrentState.Enter();
                CurrentState.Update();
                OnChanged?.Invoke();
               
            }
            else
            {
                Debug.LogError( $"Error : Can't set state to GameFSM {state} not found" );
            }
        }
        public void Dispose()
        {
            OnChanging?.Invoke();
            CurrentState?.Dispose();
            CurrentState = null;
        }
    }
}