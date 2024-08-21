using McGuard.src.handlers;
using McGuard.src.utils;
using System;
using System.Diagnostics;
using System.Threading;

namespace McGuard.src.core
{
    internal class ServerManager
    {
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

        /// <summary>
        /// Instance of outputhandler to handle outputs from server console
        /// </summary>
        private OutputHandler outputHandler;

        public ServerManager(int maximumMemory, string jarName, string workingDirectory)
        {
            this.maximumMemory = maximumMemory;
            this.jarName = jarName;
            this.workingDirectory = workingDirectory;
        }

        /// <summary>
        /// Create server process
        /// </summary>
        public void CreateServerProcess()
        {
            serverProcess = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java.exe",
                    Arguments = "-Xmx" + maximumMemory + "M -jar " + jarName + " nogui",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            serverProcess.Start();
            serverProcess.BeginOutputReadLine();

            serverProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) => outputHandler.OnDataReceive(e);

            this.outputHandler = new OutputHandler(serverProcess);

            IPC ipc = new IPC(serverProcess, ConfigManager.GetValueByKey("server-port"));
            new Thread(ipc.Start).Start();

            while (true)
            {
                string command = Console.ReadLine();
                serverProcess.StandardInput.WriteLine(command);

                if (command.Equals("stop", StringComparison.CurrentCultureIgnoreCase))
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
