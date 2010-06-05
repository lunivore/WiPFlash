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
        private delegate Application HandlerForNoMatchingProcesses(string message);
        public delegate void FailureHandler(string message);

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

        public Application Launch(string path, FailureHandler failureHandler)
        {
            try
            {
                Process process = Process.Start(new ProcessStartInfo(path));
                process.WaitForInputIdle(100);
                return new Application(process, Timeout);
            } catch (Win32Exception e)
            {
                failureHandler("Could not start application: " + e.Message);
            }
            return null;
        }

        public Application Recycle(string name, FailureHandler failureHandler)
        {
            return RecycleOrHandleNone(name, 
                s => 
                { 
                    failureHandler(s);
                    return null; 
                }, 
                failureHandler);
        }

        public Application LaunchOrRecycle(string name, string path, FailureHandler failureHandler)
        {
            return RecycleOrHandleNone(name, s => Launch(path, failureHandler), failureHandler);
        }

        private Application RecycleOrHandleNone(string name, HandlerForNoMatchingProcesses handler, FailureHandler failureHandler)
        {
            Process[] processes = Process.GetProcessesByName(name);

            List<Process> viableProcesses = FilterForViableProcesses(processes);

            if (viableProcesses.Count > 1)
            {
                failureHandler("Cannot choose between two or more processes called " + name);
                return null;
            }
            if (viableProcesses.Count < 1)
            {
                 return handler("Cannot find any processes called " + name);
            }
            return new Application(viableProcesses[0], Timeout);
        }


        private List<Process> FilterForViableProcesses(IEnumerable<Process> processes)
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