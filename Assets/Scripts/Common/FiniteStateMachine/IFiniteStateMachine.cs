namespace Common
{
    public interface IFiniteStateMachine< out TObjectState >
        where TObjectState : IFiniteStateMachineObject
    {
        TObjectState CurrentState { get; }
    }
}