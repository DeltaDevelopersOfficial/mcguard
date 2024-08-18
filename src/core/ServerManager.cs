using System;
using System.Diagnostics;

namespace McGuard.src.core
{
    internal class ServerManager
    {
        /// <summary>
        /// Initial memory for server
        /// </summary>
        private readonly int initialMemory;

        /// <summary>
        /// Maximum memory for server
        /// </summary>
        private readonly int maximumMemory;

        /// <summary>
        /// Name of the .jar file of server
        /// </summary>
        private readonly string jarName;

        /// <summary>
        /// Working directory of server
        /// </summary>
        private readonly string workingDirectory;

        /// <summary>
        /// Instance of process
        /// </summary>
        private Process serverProcess;

        public ServerManager(int initialMemory, int maximumMemory, string jarName, string workingDirectory)
        {
            this.initialMemory = initialMemory;
            this.maximumMemory = maximumMemory;
            this.jarName = jarName;
            this.workingDirectory = workingDirectory;
        }

        /// <summary>
        /// Create server process
        /// </summary>
        public void CreateProcess()
        {
            serverProcess = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            serverProcess.Start();
            serverProcess.BeginOutputReadLine();

            serverProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine(e.Data);
        }
    }
}
