using McGuard.src.test;
using McGuard.src.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProcessUtil.KillProcessByName("java", true);

            // ONLY FOR TESTING PURPOSES
            Testing.Test();
        }
    }
}
