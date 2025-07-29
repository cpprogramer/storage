namespace Common.Messages
{
    public abstract class BaseRecordMessage : IMessage
    {
        public float TimeStamp { get; set; }
    }
}