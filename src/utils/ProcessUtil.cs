using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.utils
{
    internal class ProcessUtil
    {
        /// <summary>
        /// Kills process(es) by name
        /// </summary>
        /// <param name="name">Process name</param>
        /// <param name="killAllAssociatedProcesses">Kills all process with specified name if TRUE</param>
        public static void KillProcessByName(string name, bool killAllAssociatedProcesses)
        {
            foreach (var process in Process.GetProcesses().Where(p => p.ProcessName == name))
            {
                process.Kill();

                if (!killAllAssociatedProcesses)
                {
                    break;
                }
            }
        }
    }
}
