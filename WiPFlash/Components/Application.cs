#region

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Exceptions;

#endregion

namespace WiPFlash.Components
{
    public class Application
    {
        private readonly Process _process;
        private readonly TimeSpan _timeout;
        private readonly object _waitingRoom = new object();

        /**
         * An application may be started without an attached process if you want
         * to use an existing application window.
         */
        public Application() : this(null)
        {
            
        }

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
            var windowElement = FindOrWaitForOpenWindow(windowName);

            if (windowElement == null)
            {
                string message = "Failed to find window with name " + windowName + " in timespan " + _timeout;
                throw new FailureToFindException(message);
            }
            ((WindowPattern) windowElement.GetCurrentPattern(WindowPattern.Pattern))
                .WaitForInputIdle(5000);

            return new Window(windowElement);
        }

        private AutomationElement FindOrWaitForOpenWindow(string windowName)
        {

            Monitor.Enter(_waitingRoom);
            AutomationElement windowElement;

            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent,
                                                 AutomationElement.RootElement, TreeScope.Children,
                                                 delegate { windowElement = WindowOpened(windowName); });

            windowElement = FindOpenWindow(windowName);
            if (windowElement == null)
            {
                Monitor.Wait(_waitingRoom, _timeout);
            }
            Monitor.Exit(_waitingRoom);
            return windowElement;
        }

        private AutomationElement FindOpenWindow(string windowName)
        {
            Condition condition = new PropertyCondition(AutomationElement.AutomationIdProperty, windowName);
            if (_process != null)
            {
                condition = new AndCondition(new PropertyCondition(AutomationElement.ProcessIdProperty, _process.Id),
                                             condition);
            }
            return AutomationElement.RootElement.FindFirst(TreeScope.Children,
                                                           condition);
        }

        private AutomationElement WindowOpened(string windowName)
        {
            Monitor.Enter(_waitingRoom);
            AutomationElement element = FindOpenWindow(windowName);
            if (element != null)
            {
                Monitor.Pulse(_waitingRoom);
                Monitor.Exit(_waitingRoom);
                return element;
            }
            Monitor.Exit(_waitingRoom);
            return null;
        }
    }
}