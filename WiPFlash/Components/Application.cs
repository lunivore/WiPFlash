#region

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Exceptions;
using WiPFlash.Framework;

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
            return FindWindow(FindBy.UiAutomationId(windowName));
        }

        public Window FindWindow(Condition condition)
        {
            return FindWindow(condition, _timeout,
                              message => { throw new FailureToFindException(message); });
        }

        public Window FindWindow(string windowName, TimeSpan timeout, FailureToFindHandler handler)
        {
            return FindWindow(FindBy.UiAutomationId(windowName), timeout, handler);
        }

        public Window FindWindow(Condition condition, TimeSpan timeout, FailureToFindHandler handler)
        {
            string windowName = new ConditionDescriber().Describe(condition);
            var windowElement = FindOrWaitForOpenWindow(condition, timeout);

            if (windowElement == null)
            {
                string message = "Failed to find window with name " + windowName + " in timespan " + timeout;
                handler(message);
                return null;
            }
            ((WindowPattern)windowElement.GetCurrentPattern(WindowPattern.Pattern))
                .WaitForInputIdle(5000);

            return new Window(windowElement, windowName);
        }

        private AutomationElement FindOrWaitForOpenWindow(Condition condition, TimeSpan timeout)
        {
            DateTime startedAt = DateTime.Now;
            Monitor.Enter(_waitingRoom);
            AutomationElement windowElement;

            AutomationEventHandler handler = delegate { windowElement = WindowOpened(condition); };
            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent,
                                                 AutomationElement.RootElement, TreeScope.Children,
                                                 handler);

            windowElement = FindOpenWindow(condition);
            while (windowElement == null && (DateTime.Now.Subtract(startedAt)).CompareTo(timeout) < 0)
            {
                // We are polling because sometimes the event handler doesn't fire 
                // quickly enough for my liking - the system is too busy. This lets 
                // us check every second, while still taking advantage of the event handling
                // if it does decide to fire.
                Monitor.Wait(_waitingRoom, Math.Min(1000, timeout.Milliseconds));
            }

            Automation.RemoveAutomationEventHandler(
                WindowPattern.WindowOpenedEvent, 
                AutomationElement.RootElement, 
                handler);

            Monitor.Exit(_waitingRoom);
            return windowElement;
        }

        private AutomationElement FindOpenWindow(Condition ourWindow)
        {
            Condition ourProcess = Condition.TrueCondition;
            if (_process != null)
            {
                ourProcess = new PropertyCondition(AutomationElement.ProcessIdProperty, _process.Id);
            }
            return AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new AndCondition(ourWindow, ourProcess));
        }

        private AutomationElement WindowOpened(Condition condition)
        {
            Monitor.Enter(_waitingRoom);
            AutomationElement element = FindOpenWindow(condition);
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