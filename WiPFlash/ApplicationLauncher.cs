#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using WiPFlash.Component;
using WiPFlash.Exceptions;

#endregion

namespace WiPFlash
{
    public class ApplicationLauncher
    {
        public ApplicationLauncher() : this(500) {}

        private ApplicationLauncher(int timeToLaunchInMillis)
        {
            TimeToLaunchInMillis = timeToLaunchInMillis;
        }

        public int TimeToLaunchInMillis
        {
            get; set;
        }

        public Application Launch(string path)
        {
            try
            {
                Process process = Process.Start(new ProcessStartInfo(path));
                Thread.Sleep(TimeToLaunchInMillis);
                return new Application(process);
            } catch (Win32Exception e)
            {
                string message = "Could not find the process to start on path \"" + path + "\". Current directory is \"" +
                                 Environment.CurrentDirectory + "\".";
                throw new FailureToLaunchException(e, message);
            }
        }

        public Application Recycle(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length > 1)
            {
                throw new FailureToLaunchException("Cannot choose between two or more processes called " + name);                
            }
            return new Application(processes[0]);
        }

        public Application LaunchOrRecycle(string name, string path)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length == 0)
            {
                Launch(path);
            } else if (processes.Length > 1)
            {
                throw new FailureToLaunchException("Cannot choose between two or more processes called " + name);
            } else
            {
                return new Application(processes[0]);
            }
            return null; // Um, VS, why do you need this here?!
        }
    }
}