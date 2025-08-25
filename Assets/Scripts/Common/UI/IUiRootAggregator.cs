using UniRx;

namespace Common.UI
{
    public interface IUiRootAggregator
    {
        int InstanceID { get; }
        IMessageBroker MessageBroker { get; }

        IUserActionsQueue UserActionsQueue { get; }
    }
}