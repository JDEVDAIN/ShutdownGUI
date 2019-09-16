using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownFrameworkWPF
{
    static class ShutdownExecutor
    {

        public static void ExecuteShutdown(string parameters, string delay, bool forcedShutdown)
        {

            var psi = new ProcessStartInfo("shutdown", $"/s {(forcedShutdown ? "/f" : "")} {parameters} /t {delay}")
            {
                CreateNoWindow = true, UseShellExecute = false
            };
            Process.Start(psi);
        }

        public static void ExecuteCustomShutdown(string customReason)
        {
            var psi = new ProcessStartInfo("shutdown", customReason)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi);
        }

        public static void CancelShutdown()
        {
            var psi = new ProcessStartInfo("shutdown", "/a")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi);
        }

    }
}
