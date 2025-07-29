namespace Common.UI.Model
{
    public interface IWindowModel< out TLogicType >
        where TLogicType : class
    {
        TLogicType TypeWindow { get; }
        int Id { get; }
        bool IsModal { get; }
    }
}