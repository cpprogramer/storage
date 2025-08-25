namespace Common
{
    public class ProcessingSequenceCommands : ProcessingCommands
    {
        private bool _canExecute = true;
        private ICommand _currentActive;

        public override void StartExecute() => Execute();

        public override void ForceFinish()
        {
            base.ForceFinish();
            _currentActive?.Dispose();
        }

        protected override void Execute()
        {
            if ( _canExecute )
            {
                _canExecute = false;

                _currentActive = Dequeue();
                if ( _currentActive != null )
                {
                    _currentActive.OnCompleted += CompletedHandler;
                    _currentActive.Do();
                }
                else
                {
                    _canExecute = true;
                    OnAllCommandsDone();
                }
            }
        }

        private void CompletedHandler()
        {
            _currentActive.OnCompleted -= CompletedHandler;
            _currentActive = null;
            _canExecute = true;
            Execute();
        }
    }
}