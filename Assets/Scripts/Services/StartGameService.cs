using Common;
using FSM;
using System;

namespace MonopolySpace.Lobby
{
    public sealed class StartGameService : IStartGameService
    {
        private readonly IFSM _fsm;

        public StartGameService( IFSM fsm ) => _fsm = fsm ?? throw new ArgumentNullException( nameof(fsm) );

        public void StartGame( StartGameModel model ) => _fsm.SetState( typeof(GameFSMObject), model );
    }
}