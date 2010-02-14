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
        private delegate Application HandlerForNoMatchingProcesses();

        public ApplicationLauncher() : this(500) {}

        public ApplicationLauncher(int timeToLaunchInMillis)
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
            return RecycleOrHandleHavingNone(name, () => {
                 throw new FailureToFindException(
                     "Could not find an existing application with name " + name +
                     " to recycle"); });

        }

        public Application LaunchOrRecycle(string name, string path)
        {
            return RecycleOrHandleHavingNone(name, () => Launch(path));
        }

        private Application RecycleOrHandleHavingNone(string name, HandlerForNoMatchingProcesses handleNoMatchingProcesses)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length > 1)
            {
                throw new FailureToLaunchException("Cannot choose between two or more processes called " + name);
            }
            if (processes.Length < 1)
            {
                return handleNoMatchingProcesses();
            }
            return new Application(processes[0]);
        }
    }
}