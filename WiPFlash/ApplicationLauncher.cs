#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Exceptions;
using WiPFlash.Framework;

#endregion

namespace WiPFlash
{
    public class ApplicationLauncher
    {
        private delegate Application HandlerForNoMatchingProcesses();

        public ApplicationLauncher() : this(Window.DEFAULT_TIMEOUT) {}

        public ApplicationLauncher(TimeSpan timeout) : this(timeout, new NameBasedFinder(new WrapperFactory()))
        {
        }

        public ApplicationLauncher(TimeSpan timeout, IFindAutomationElements finder) {
            Timeout = timeout;
            Finder = finder;
        }

        public TimeSpan Timeout
        {
            get; set;
        }

        public IFindAutomationElements Finder
        {
            get; set;
        }

        public Application Launch(string path)
        {
            try
            {
                Process process = Process.Start(new ProcessStartInfo(path));
                process.WaitForInputIdle(100);
                return new Application(process, Timeout);
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

            List<Process> viableProcesses = FilterForViableProcesses(processes);

            if (viableProcesses.Count > 1)
            {
                throw new FailureToLaunchException("Cannot choose between two or more processes called " + name);
            }
            if (viableProcesses.Count < 1)
            {
                return handleNoMatchingProcesses();
            }
            return new Application(viableProcesses[0], Timeout);
        }

        private List<Process> FilterForViableProcesses(Process[] processes)
        {
            var viableProcesses = new List<Process>();

            foreach (var process in processes)
            {
                var windowWithProcess = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                        new AndCondition(
                            new PropertyCondition(AutomationElement.ProcessIdProperty,
                                                  process.Id),
                            new PropertyCondition(
                                AutomationElement.ControlTypeProperty,
                                ControlType.Window)));
                if (windowWithProcess != null)
                {
                    viableProcesses.Add(process);
                }
            }
            return viableProcesses;
        }
    }
}