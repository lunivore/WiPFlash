#region

using System;
using System.Windows.Automation;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Components
{
    public class Window : Container
    {
        public static readonly TimeSpan DEFAULT_TIMEOUT = TimeSpan.Parse("00:00:10");
        public static readonly int DEFAULT_TIMEOUT_IN_MILLIS = 10000;
        private int _processId;

        public Window(AutomationElement element, string name) : this(element, name, new NameBasedFinder(new WrapperFactory()))
        {
        }

        public Window(AutomationElement element, IFindAutomationElements automationIdBasedFinder) : this(element, string.Empty, automationIdBasedFinder)
        {
        }

        public Window(AutomationElement element, string name, IFindAutomationElements finder) : base(element, name, finder)
        {
            _processId = int.Parse(element.GetCurrentPropertyValue(AutomationElement.ProcessIdProperty).ToString());
        }

        public void Close()
        {
            ((WindowPattern)Element.GetCurrentPattern(WindowPattern.Pattern))
                .Close();
        }

        public bool IsClosed()
        {
            return AutomationElement.RootElement.FindFirst(TreeScope.Children, 
                new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                new PropertyCondition(AutomationElement.ProcessIdProperty, _processId))) == null;
        }
    }
}