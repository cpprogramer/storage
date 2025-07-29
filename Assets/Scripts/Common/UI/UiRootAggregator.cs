using UniRx;

namespace Common.UI
{
    public sealed class UiRootAggregator : IUiRootAggregator
    {
        public int InstanceID { get; }
        public IUiRootViewModel RootViewModel { get; }
        public IMessageBroker MessageBroker { get; }

        public UiRootAggregator( int instanceID, IUiRootViewModel rootViewModel, IMessageBroker messageBroker )
        {
            InstanceID = instanceID;
            RootViewModel = rootViewModel;
            MessageBroker = messageBroker;
        }
    }
}