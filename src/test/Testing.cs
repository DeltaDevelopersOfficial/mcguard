using McGuard.src.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.test
{
    internal class Testing
    {
        public static void Test()
        {

            ServerManager sm = new ServerManager(512, "server.jar", Environment.CurrentDirectory);

            sm.CreateServerProcess();

            Console.CancelKeyPress += Console_CancelKeyPress;

        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
