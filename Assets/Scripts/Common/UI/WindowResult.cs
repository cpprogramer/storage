using System;

namespace Common.UI
{
    [ Flags ]
    public enum WindowResult
    {
        Undefined = 0x0,
        TryAgain = 0x1,
        Back = 0x2,
        Quit = 0x4,
        Yes = 0x8,
        No = 0x10,
        Cancel = 0x20
    }
}