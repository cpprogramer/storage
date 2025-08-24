using System;
using UniRx;

namespace Common.UI
{
    public sealed class UiRootAggregator : IUiRootAggregator
    {
        public int InstanceID { get; }
        public IUiRootViewModel RootViewModel { get; }
        public IMessageBroker MessageBroker { get; }
        public IUserActionsQueue UserActionsQueue { get; }

        public UiRootAggregator(
            int instanceID,
            IUiRootViewModel rootViewModel,
            IMessageBroker messageBroker,
            IUserActionsQueue userActionsQueue
        )
        {
            InstanceID = instanceID;
            RootViewModel = rootViewModel ?? throw new ArgumentNullException( nameof(rootViewModel) );
            MessageBroker = messageBroker ?? throw new ArgumentNullException( nameof(messageBroker) );
            UserActionsQueue = userActionsQueue ?? throw new ArgumentNullException( nameof(userActionsQueue) );
        }
    }
}