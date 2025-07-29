using Common;
using Common.UI;
using Common.UI.Messages;
using Cysharp.Threading.Tasks;
using MonopolySpace.Messages;
using MonopolySpace.UI.Controllers;
using System;
using UniRx;

namespace FSM
{
    public sealed class LoadingFSMObject : BaseFiniteStateMachineObject
    {
        private const int WAIT_TIME_IN_SEC = 2;

        private readonly IMessageBroker _messageBroker;
        
        public LoadingFSMObject( IFSM parentFsm, IMessageBroker messageBroker ) : base( parentFsm )
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof( messageBroker ) );
        }

        public override void Dispose()
        {
            _messageBroker.Publish(  new UICloseWindowMessage( typeof(UILoadingController), WindowResult.Back ) );
        }

        protected override void OnStart()
        {
            Simulate().Forget();
        }

        private async UniTaskVoid Simulate()
        {
            _messageBroker.Publish(  new UIOpenWindowMessage( new UILoadingWindowModel()) );
            await UniTask.Delay( TimeSpan.FromSeconds( WAIT_TIME_IN_SEC ) );
            _parentFsm.SetState( typeof(MainMenuFSMObject), DummyModel.Dummy );
        }
    }
}