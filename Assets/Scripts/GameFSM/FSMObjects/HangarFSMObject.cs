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
        private readonly HangarGamePlayManager _hangarGamePlayManager;

        public HangarFSMObject(
            IFSM parentFsm,
            IMessageBroker messageBroker,
            IScenesManager scenesManager,
            int instanceId
        ) : base( parentFsm )
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            _hangarGamePlayManager = new HangarGamePlayManager( scenesManager, instanceId );
        }

        public override void Dispose() {}

        protected override void OnStart()
        {
            _hangarGamePlayManager.Start();
        }
        
    }
}