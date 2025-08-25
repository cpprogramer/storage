using Common;
using Common.UI;
using Common.UI.Messages;
using Cysharp.Threading.Tasks;
using StorageTest.Messages;
using StorageTest.UI.ViewModel;
using System;
using UniRx;


namespace FSM
{
    public sealed class LoadingFSMObject : BaseFiniteStateMachineObject
    {
        private const int WAIT_TIME_IN_SEC = 2;

        private readonly IMessageBroker _messageBroker;

        public LoadingFSMObject( IFSM parentFsm, IMessageBroker messageBroker ) : base( parentFsm ) =>
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );

        public override void Dispose() =>
            _messageBroker.Publish( new UICloseWindowMessage( typeof(UILoadingViewModel), WindowResult.Back ) );

        protected override void OnStart() => Simulate().Forget();

        private async UniTaskVoid Simulate()
        {
            _messageBroker.Publish( new UIOpenWindowMessage( new UILoadingWindowDTO() ) );
            await UniTask.Delay( TimeSpan.FromSeconds( WAIT_TIME_IN_SEC ) );
            _parentFsm.SetState( typeof(MainMenuFSMObject), DummyDTO.Dummy );
        }
    }
}