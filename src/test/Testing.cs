using McGuard.src.core;
using McGuard.src.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace McGuard.src.test
{
    internal class Testing
    {
        public static void Test()
        {
            ConfigManager.LoadConfiguration("mcguard.ini");
            ConfigManager.LoadConfiguration("server.properties");

            ServerManager sm = new ServerManager(256, "server.jar", Environment.CurrentDirectory);
            sm.CreateServerProcess();
        }
    }
}
