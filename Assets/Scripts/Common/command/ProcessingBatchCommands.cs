namespace Common
{
    public class ProcessingBatchCommands : ProcessingCommands
    {
        protected override void Execute()
        {
            while ( true )
            {
                ICommand cmd = Dequeue();
                if ( cmd == null )
                    break;

                cmd.Do();
            }

            OnAllCommandsDone();
        }
    }
}