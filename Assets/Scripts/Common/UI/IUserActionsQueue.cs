namespace Common.UI
{
    public interface IUserActionsQueue
    {
        public void AddRequest( IBaseMessage message );
    }
}