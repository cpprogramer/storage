using System;

namespace MonopolySpace.Model
{
    public sealed class RoomData
    {
        public const int MaxCountActors = 4;
        public event Action OnChanged;

        public readonly string UId;
        public bool IsClosed => CountActors == MaxCountActors;

        public int CountActors { get; private set; }

        public RoomData( string uid ) => UId = uid;

        public void Update( int countActors )
        {
            CountActors = countActors;
            OnChanged?.Invoke();
        }
    }
}