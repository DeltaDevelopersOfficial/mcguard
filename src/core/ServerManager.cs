using McGuard.src.content;
using McGuard.src.core.providers;
using McGuard.src.handlers;
using McGuard.src.listeners;
using McGuard.src.structures;
using McGuard.src.structures.enums;
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

        /// <summary>
        /// Instance of command listener
        /// </summary>
        private CommandListener commandListener;

        public ServerManager(int maximumMemory, string jarName, string workingDirectory)
        {
            this.maximumMemory = maximumMemory;
            this.jarName = jarName;
            this.workingDirectory = workingDirectory;
        }

        public void Setup()
        {
            this.outputHandler = new OutputHandler(serverProcess);
            this.commandListener = new CommandListener(serverProcess);

            IPC ipc = new IPC(serverProcess, ConfigManager.GetValueByKey("server-port"));
            new Thread(ipc.Start).Start();

            if (int.TryParse(ConfigManager.GetValueByKey("maxcpuaffinity"), out int cpuCores))
            {
                // if is higher, set limit
                // but if it's less, use all cores (default)
                if (cpuCores > 0)
                {
                    ProcessUtil.SetProcessAffinity(cpuCores, serverProcess);
                }
            }
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

            Setup();

            serverProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) => outputHandler.OnDataReceive(e);

            while (!serverProcess.HasExited)
            {
                string input = Console.ReadLine();
                
                if (input.StartsWith("!") && input.Trim().Length > 0)
                {
                    Command command = new Command(input, input.Split(' '));

                    CommandResult commandResult = commandListener.OnConsoleCommand(command);

                    if (commandResult.HasFlag(CommandResult.Success))
                    {
                        if (commandResult.HasFlag(CommandResult.NotAvailableFromConsole))
                        {
                            Console.WriteLine(StringManager.GetString(14).Replace("%s", "console"));
                        }
                        else
                        {
                            // ...
                        }
                    }
                    else if (commandResult.HasFlag(CommandResult.Failed))
                    {
                        Console.WriteLine(StringManager.GetString(15).Replace("%s", command.Name));
                    }
                }
                else
                {
                    serverProcess.StandardInput.WriteLine(input);

                    if (input.Equals("stop", StringComparison.CurrentCultureIgnoreCase))
                    {
                        new Thread(() =>
                        {
                            Thread.Sleep(500);
                            Environment.Exit(0);
                        }).Start();
                    }
                }
            }
        }
    }
}
