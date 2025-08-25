using Common;
using FSM;
using System;

namespace StorageTest.Lobby
{
    public sealed class StartGameService : IStartGameService
    {
        private readonly IFSM _fsm;

        public StartGameService( IFSM fsm ) => _fsm = fsm ?? throw new ArgumentNullException( nameof(fsm) );

        public void StartGame( StartGameDTO dto ) => _fsm.SetState( typeof(GameFSMObject), dto );
    }
}