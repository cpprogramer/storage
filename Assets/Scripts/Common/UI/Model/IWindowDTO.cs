namespace Common.UI.Model
{
    public interface IWindowDTO< out TLogicType >
        where TLogicType : class
    {
        TLogicType TypeWindow { get; }
        bool IsModal { get; }
    }
}