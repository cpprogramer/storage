using System;

namespace MonopolySpace.Net
{
    public interface IGameplayMessaging : IGameplayMessaging< MessageCode >
    {
    }

    public enum ReceiverGroupCustom : byte
    {
        /// <summary>Default value (not sent). Anyone else gets my event.</summary>
        Others = 0,

        /// <summary>Everyone in the current room (including this peer) will get this event.</summary>
        All = 1,

        /// <summary>The server sends this event only to the actor with the lowest actorNumber.</summary>
        /// <remarks>The "master client" does not have special rights but is the one who is in this room the longest time.</remarks>
        MasterClient = 2
    }

    public interface IGameplayMessaging< TCode > : IDisposable
        where TCode : struct
    {
        event Action< MessageData > OnEventReceived;
        bool IsMasterClient { get; }
        void Enable();
        void Disable();
        void SendEvent( TCode eventCode, object message, ReceiverGroupCustom receiverGroup );
        bool IsLocalPlayer( string id );
    }
}