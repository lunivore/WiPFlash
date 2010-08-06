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

        public Application(Process process) : this(process, Window.DefaultTimeout)
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

            return new Window(windowElement, windowName);
        }

        private AutomationElement FindOrWaitForOpenWindow(string windowName)
        {
            DateTime startedAt = DateTime.Now;
            Monitor.Enter(_waitingRoom);
            AutomationElement windowElement;

            AutomationEventHandler handler = delegate { windowElement = WindowOpened(windowName); };
            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent,
                                                 AutomationElement.RootElement, TreeScope.Children,
                                                 handler);

            windowElement = FindOpenWindow(windowName);
            while (windowElement == null && (DateTime.Now.Subtract(startedAt)).CompareTo(_timeout) < 0)
            {
                // We are polling because sometimes the event handler doesn't fire 
                // quickly enough for my liking - the system is too busy. This lets 
                // us check every second, while still taking advantage of the event handling
                // if it does decide to fire.
                Monitor.Wait(_waitingRoom, 1000);
            }

            Automation.RemoveAutomationEventHandler(
                WindowPattern.WindowOpenedEvent, 
                AutomationElement.RootElement, 
                handler);

            Monitor.Exit(_waitingRoom);
            return windowElement;
        }

        private AutomationElement FindOpenWindow(string windowName)
        {
            Condition ourWindow = new PropertyCondition(AutomationElement.AutomationIdProperty, windowName);
            Condition ourProcess = Condition.TrueCondition;
            if (_process != null)
            {
                ourProcess = new PropertyCondition(AutomationElement.ProcessIdProperty, _process.Id);
            }
            return AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new AndCondition(ourWindow, ourProcess));
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