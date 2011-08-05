using System;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Framework;

namespace WiPFlash.Components
{
    public class WindowFinder
    {
        private readonly object _waitingRoom = new object();

        public Window FindWindow(Condition condition, TimeSpan timeout, FailureToFindHandler handler)
        {
            string windowName = new ConditionDescriber().Describe(condition);
            var windowElement = FindOrWaitForOpenWindow(condition, timeout);

            if (windowElement == null)
            {
                string message = "Failed to find window with " + windowName + " in timespan " + timeout;
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
        
        private AutomationElement FindOpenWindow(Condition condition)
        {
            return AutomationElement.RootElement.FindFirst(
                TreeScope.Children, condition);
        }
    }
}