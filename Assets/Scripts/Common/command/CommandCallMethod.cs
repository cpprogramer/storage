using System;

namespace Common
{
    public class CommandCallMethod : Command
    {
        private readonly Action< Action > action = delegate {};

        public CommandCallMethod( string key, Action< Action > ac )
        {
            CommandKey = key;
            action = ac;
        }

        public override void Do() => action( Complete );

        public void DBG()
        {
            // Debug.LogError(action.Method.Name);
        }
    }
}