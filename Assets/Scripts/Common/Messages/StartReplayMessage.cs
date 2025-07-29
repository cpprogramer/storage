namespace Common.Messages
{
    public sealed class StartReplayMessage : IMessage
    {
        public string FileName { get; }

        public StartReplayMessage( string fileName ) => FileName = fileName;
    }
}