using Common;
using Common.UI;
using Common.UI.Messages;
using Cysharp.Threading.Tasks;
using StorageTest.Net;
using StorageTest.UI.Controllers;
using System;

namespace FSM
{
    public sealed class WaitPlayersFSMObject : BaseFiniteStateMachineObject
    {
        public WaitPlayersFSMObject( IFSM parentFsm, IMultiplayerService multiplayerService) : base( parentFsm ) {}


        //_multiplayerBackend = multiplayerBackend ?? throw new ArgumentNullException( nameof(multiplayerBackend) );
        public override void Dispose()
        {
            //_uiManager.CloseWindow( new UICloseWindowMessage( typeof(UIWaitPlayersController), WindowResult.Back ) );
        }

        protected override void OnInitialize()
        {
            //_uiManager.OnOpeningFinished += OpeningFinishedHandler;
            //_uiManager.ShowWindow( new UIOpenWindowMessage( new UIWaitPlayersModel() ) );

            void OpeningFinishedHandler( UIOpenedWindowMessage uiOpenedWindowMessage )
            {
                //_uiManager.OnOpeningFinished -= OpeningFinishedHandler;
                /*if ( uiOpenedWindowMessage.Window.TypeWindow == typeof(UIWaitPlayersController) )
                    _multiplayerBackend.Connect();*/
            }
        }
    }
}