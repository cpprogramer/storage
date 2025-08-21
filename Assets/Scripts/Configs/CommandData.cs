using System;
using UnityEngine;

namespace StorageTest.Configs
{
    [ Serializable ]
    public sealed class CommandData
    {
        [ SerializeField ] private CommandHandler _commandHandler;
        [ SerializeField ] private string _arguments;

        public CommandHandler GetCommandHandler()
        {
            var instance = ScriptableObject.CreateInstance( _commandHandler.GetType() ) as CommandHandler;
            instance.SetupArgs( _arguments );
            return instance;
        }
    }
}