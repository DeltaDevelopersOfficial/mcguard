using McGuard.src.handlers;
using McGuard.src.structures;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.core.providers
{
    internal class MacroProvider
    {
        /// <summary>
        /// Input handler instance
        /// </summary>
        private InputHandler inputHandler;

        /// <summary>
        /// Filename of macro
        /// </summary>
        private string fileName;

        /// <summary>
        /// Process instance
        /// </summary>
        private Process process;

        /// <summary>
        /// Macro instance
        /// </summary>
        private Macro macro;

        /// <summary>
        /// Macro scripts directory
        /// </summary>
        private string macroDirectory = Environment.CurrentDirectory + "\\macros";

        /// <summary>
        /// Full path with directory and with script file
        /// </summary>
        public string finalPath;

        public MacroProvider(Process process, string fileName)
        {
            this.fileName = fileName;
            this.process = process;
            this.finalPath = macroDirectory + "\\" + fileName;
            this.inputHandler = new InputHandler(process);
            this.Setup();
        }

        /// <summary>
        /// Setup a basics
        /// </summary>
        private void Setup()
        {
            if (!Directory.Exists(macroDirectory))
            {
                Directory.CreateDirectory(macroDirectory);
            }

            if (Exists())
            {
                this.macro = new Macro(File.ReadAllLines(finalPath).ToList());
            }
        }

        /// <summary>
        /// Checks if exists
        /// </summary>
        /// <returns>Return TRUE if macro file exists (do not include directory)</returns>
        public bool Exists()
        {
            return File.Exists(finalPath);
        }

        /// <summary>
        /// Checks if is script empty file
        /// </summary>
        /// <returns>Returns TRUE if scrpit doesnt contains any commands</returns>
        public bool IsEmpty()
        {
            return macro.commandList.Count == 0;
        }

        /// <summary>
        /// Executes a macro script
        /// </summary>
        public void Execute()
        {
            foreach (var command in macro.commandList)
            {
                inputHandler.SendInput(command);
            }
        }
    }
}
