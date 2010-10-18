#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace WiPFlash
{
    public class ApplicationLauncher
    {
        private delegate Application HandlerForNoMatchingProcesses(string message);
        private delegate List<Process> ViableProcessFilter(IEnumerable<Process> processes);

        public delegate void FailureToLaunchHandler(string message);

        public ApplicationLauncher() : this(Window.DefaultTimeout) {}

        public ApplicationLauncher(TimeSpan timeout) : this(timeout, new ConditionBasedFinder(new WrapperFactory(), new ConditionDescriber()))
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

        public Application Launch(string path, FailureToLaunchHandler failureToLaunchHandler)
        {
            try
            {
                Process process = Process.Start(new ProcessStartInfo(path));
                return new Application(process, Timeout);
            } catch (Win32Exception e)
            {
                failureToLaunchHandler("Could not start application: " + e.Message);
            }
            return null;
        }

        public Application Recycle(string name, FailureToLaunchHandler failureToLaunchHandler)
        {
            return RecycleOrHandleNone(name, FilterForViableProcesses,
                s => 
                { 
                    failureToLaunchHandler(s);
                    return null; 
                }, 
                failureToLaunchHandler);
        }

        public Application LaunchOrRecycle(string name, string path, FailureToLaunchHandler failureToLaunchHandler)
        {
            return RecycleOrHandleNone(name, FilterForViableProcesses, s => Launch(path, failureToLaunchHandler), failureToLaunchHandler);
        }

        private Application RecycleOrHandleNone(string name, ViableProcessFilter filter, HandlerForNoMatchingProcesses handler, FailureToLaunchHandler failureToLaunchHandler)
        {
            Process[] processes = Process.GetProcessesByName(name);

            List<Process> viableProcesses = filter(processes);

            if (viableProcesses.Count > 1)
            {
                failureToLaunchHandler("Cannot choose between two or more processes called " + name);
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

        public Application LaunchVia(string name, string path, FailureToLaunchHandler failureHandler)
        {
            DateTime started = DateTime.Now;
            var existingProcesses = new List<Process>(Process.GetProcessesByName(name));
            
            Process.Start(new ProcessStartInfo(path));

            while (DateTime.Now.Subtract(started).CompareTo(Timeout) < 0)
            {
                var newProcesses = new List<Process>(Process.GetProcessesByName(name));
                newProcesses.RemoveAll(p => existingProcesses.Find(p2 => p2.Id == p.Id) != null);
                if (newProcesses.Count > 0) { return new Application(newProcesses[0], Timeout); }
                Thread.Sleep(100);
            }
            failureHandler(String.Format("Process {0} was not launched via {1}", name, path));
            return null;
        }
    }
}