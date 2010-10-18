#region

using System;
using System.Diagnostics;
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

        /**
         * An application may be started without an attached process if you want
         * to use an existing application window.
         */
        public Application() : this(null)
        {            
        }

        public Application(TimeSpan timeout) : this(null, timeout) {}

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
            var allConditions = condition;
            if (_process != null)
            {
                allConditions = new AndCondition(condition, new PropertyCondition(AutomationElement.ProcessIdProperty, _process.Id));
            }
            return new WindowFinder().FindWindow(allConditions, timeout, handler);
        }

    }
}