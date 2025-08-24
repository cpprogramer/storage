using Common.UI.Model;

namespace Common.UI.Messages
{
    public readonly struct UIOpenWindowMessage
    {
        public readonly BaseWindowDTO Dto;

        public UIOpenWindowMessage( BaseWindowDTO dto ) => Dto = dto;
    }
}