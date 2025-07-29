using Common.Messages;

namespace MonopolySpace.UI
{
    public sealed class RollDataMessage : IMessage
    {
        public readonly int Num1;
        public readonly int Num2;

        public RollDataMessage( int num1, int num2 )
        {
            Num1 = num1;
            Num2 = num1;
        }
    }
}