namespace MonopolySpace.Model
{
    public sealed class Race
    {
        public string Uid { get; }
        public int TechnologicalAdvancement { get; }

        public Race( string uid, int percent )
        {
            Uid = uid;
            TechnologicalAdvancement = percent;
        }
    }
}