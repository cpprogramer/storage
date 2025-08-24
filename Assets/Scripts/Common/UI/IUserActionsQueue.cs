using Common.Messages;

namespace Common.UI
{
    public interface IUserActionsQueue
    {
        public void AddRequest( IMessage message );
    }
}