using McGuard.src.handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.listeners
{
    internal class ConnectionListener : InputHandler
    {
        public ConnectionListener(Process process) : base (process)
        {
        }
    }
}
