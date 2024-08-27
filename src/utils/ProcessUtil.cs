using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

        /// <summary>
        /// Sets the processor affinity for a process by its name
        /// </summary>
        /// <param name="coreCount">Number of cores the process can use. Use 0 (or less) for all available cores</param>
        /// <param name="processName">The name of the process.</param>
        public static void SetProcessAffinity(int coreCount, Process serverProcess)
        {
            if (serverProcess != null && !serverProcess.HasExited)
            {
                if (coreCount <= 0)
                {
                    // Use all available cores
                    serverProcess.ProcessorAffinity = (IntPtr)(-1);
                }
                else
                {
                    // Generate a bitmask to use the specified number of cores
                    // Example: coreCount = 2 => 0b00000011 (binary) = 0x3 (hexadecimal)
                    IntPtr mask = (IntPtr)((1 << coreCount) - 1);
                    serverProcess.ProcessorAffinity = mask;
                }
            }
        }
    }
}
