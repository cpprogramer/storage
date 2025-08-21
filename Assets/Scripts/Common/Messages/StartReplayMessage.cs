namespace Common.Messages
{
    public readonly struct StartReplayMessage : IMessage
    {
        public readonly string FileName;

        public StartReplayMessage( string fileName ) => FileName = fileName;
    }
}