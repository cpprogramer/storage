using Common;
using Common.UI;
using Common.UI.Messages;
using StorageTest.Lobby;
using StorageTest.Messages;
using StorageTest.UI.ViewModel;
using System;
using UniRx;

namespace FSM
{
    public sealed class MainMenuFSMObject : BaseFiniteStateMachineObject
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IStartGameService _startGameService;
        private CompositeDisposable _compositeDisposable;

        public MainMenuFSMObject(
            IFSM parentFsm,
            IStartGameService startGameService,
            IMessageBroker messageBroker
        ) : base( parentFsm )
        {
            _startGameService = startGameService ?? throw new ArgumentNullException( nameof(startGameService) );
            _messageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
        }

        public override void Dispose()
        {
            _compositeDisposable.Dispose();
            _messageBroker.Publish( new UICloseWindowMessage( typeof(UIMainMenuViewModel), WindowResult.Back ) );
        }

        protected override void OnInitialize()
        {
            _compositeDisposable = new CompositeDisposable();
            _messageBroker.Publish( new UIOpenWindowMessage( new UIMainMenuDTO() ) );
            _messageBroker.Receive< GotoHangarMessage >().Subscribe( GotoHangarMessageHandler );
        }

        private void GotoHangarMessageHandler( GotoHangarMessage msg ) =>
            _parentFsm.SetState( typeof(HangarFSMObject) );
    }
}