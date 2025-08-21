using Common;
using Cysharp.Threading.Tasks;
using StorageTest.Model;
using System;
using UniRx;

namespace FSM
{
    public sealed class HangarFSMObject : BaseFiniteStateMachineObject
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IScenesManager _scenesManager;
        private Hangar _hangar;
        private HangarView _hangarView;
        private int _instanceId;

        public HangarFSMObject(
            IFSM parentFsm,
            IMessageBroker messageBroker,
            int instanceId,
            IScenesManager scenesManager
        ) : base( parentFsm )
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            _scenesManager = scenesManager ?? throw new ArgumentNullException( nameof(scenesManager) );
            _instanceId = instanceId;
        }

        public override void Dispose() {}

        protected override void OnStart() => CreateAndStart().Forget();

        private async UniTaskVoid CreateAndStart()
        {
            _hangar = new Hangar();
            _hangar.Initialize();
            _hangarView = new HangarView( _hangar, _scenesManager, _instanceId );
            await _hangarView.Initialize();
            _hangar.Start();
        }
    }
}