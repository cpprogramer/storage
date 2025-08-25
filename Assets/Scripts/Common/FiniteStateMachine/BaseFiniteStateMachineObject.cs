using GameFSM;

namespace Common
{
    public abstract class BaseFiniteStateMachineObject : IFiniteStateMachineObject
    {
        protected readonly IFSM _parentFsm;

        protected readonly IFSM _subFsm;

        protected BaseFiniteStateMachineObject( IFSM parentFsm )
        {
            _parentFsm = parentFsm;
            _subFsm = new GameFsm();
            _parentFsm.OnChanging += StateChangingHandler;
        }

        public abstract void Dispose();

        public void Enter()
        {
            OnCreate();
            OnInitialize();
            OnStart();
        }

        public virtual void Update() {}
        public virtual void Exit() {}
        protected virtual void OnCreate() {}
        protected virtual void OnStart() {}
        protected virtual void OnInitialize() {}

        private void StateChangingHandler()
        {
            _subFsm.Dispose();
            _parentFsm.OnChanging -= StateChangingHandler;
        }
    }
}