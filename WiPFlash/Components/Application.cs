
using System;
using System.Diagnostics;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Exceptions;

namespace WiPFlash.Component
{
    public class Application
    {
        private Process _process;

        public Application(Process process)
        {
            _process = process;
        }

        public Process Process
        {
            get { return _process; }
        }

        public Window FindWindow(string windowName)
        {
            AutomationElement windowElement = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                                                    new PropertyCondition(AutomationElement.AutomationIdProperty, windowName));
            if (windowElement == null)
            {
                string message = "Failed to find window with name " + windowName + " - found elements called: ";
                foreach (AutomationElement element in AutomationElement.RootElement.FindAll(TreeScope.Children, Condition.TrueCondition))
                {
                    message = message + element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) + ", ";
                }
                throw new FailureToFindException(message);
            }
            return new Window(windowElement);
        }
    }
}
