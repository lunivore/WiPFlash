#region

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Exceptions;

#endregion

namespace WiPFlash.Component
{
    public class Application
    {
        private Process _process;
        private TimeSpan _timeout;
        private object _waitingRoom = new object();

        public Application(Process process) : this(process, Window.DEFAULT_TIMEOUT)
        {
        }

        public Application(Process process, TimeSpan timeout)
        {
            _process = process;
            _timeout = timeout;
        }

        public Process Process
        {
            get { return _process; }
        }

        public Window FindWindow(string windowName)
        {
            Monitor.Enter(_waitingRoom);
            AutomationElement windowElement;

            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent,
                                                 AutomationElement.RootElement, TreeScope.Children,
                                                 delegate { windowElement = WaitForWindow(windowName); });

            windowElement = FindWindowElement(windowName);
            if (windowElement == null)
            {
                Monitor.Wait(_waitingRoom, _timeout);
            }
            Monitor.Exit(_waitingRoom);

            if (windowElement == null)
            {
                string message = "Failed to find window with name " + windowName + " in timespan " + _timeout;
                throw new FailureToFindException(message);
            }
            ((WindowPattern) windowElement.GetCurrentPattern(WindowPattern.Pattern))
                .WaitForInputIdle(5000);
            return new Window(windowElement);
        }

        private AutomationElement FindWindowElement(string windowName)
        {
            return AutomationElement.RootElement.FindFirst(TreeScope.Children,
                                                           new PropertyCondition(AutomationElement.AutomationIdProperty, windowName));
        }

        private AutomationElement WaitForWindow(string windowName)
        {
            Monitor.Enter(_waitingRoom);
            AutomationElement element = FindWindowElement(windowName);
            if (element != null)
            {
                Monitor.Pulse(_waitingRoom);
                return element;
            }
            Monitor.Exit(_waitingRoom);
            return null;
        }
    }
}
