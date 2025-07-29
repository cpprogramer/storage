using Common.UI.Model;

namespace Common.UI.Messages
{
    public readonly struct UIOpenWindowMessage
    {
        public readonly BaseWindowModel Model;

        public UIOpenWindowMessage( BaseWindowModel model ) => Model = model;
    }
}