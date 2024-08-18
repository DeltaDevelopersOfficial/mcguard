using System;
using System.Diagnostics;

namespace McGuard.src.utils
{
    internal class JavaChecker
    {
        /// <summary>
        /// Checks if is ANY java version installed
        /// </summary>
        /// <returns></returns>
        public static bool CheckJavaInstallation()
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo("java", "-version");

                processInfo.RedirectStandardError = true;
                processInfo.UseShellExecute = false;
                processInfo.CreateNoWindow = true;

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return !string.IsNullOrEmpty(output);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
