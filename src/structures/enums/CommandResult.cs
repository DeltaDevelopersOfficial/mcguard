using System;

namespace McGuard.src.structures.enums
{
    /// <summary>
    /// Command result
    /// </summary>
    [Flags]
    public enum CommandResult
    {
        Success = 0x1,                  // successfully command handled
        Failed = 0x2,                   // command not handled (probably because an unknown command)
        NotAvailableFromConsole = 0x4,  // handled game command from console
    }
}
