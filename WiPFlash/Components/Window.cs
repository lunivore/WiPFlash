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

        public Window(AutomationElement element) : base(element)
        {
        }

        public Window(AutomationElement element, IFindAutomationElements automationIdBasedFinder) : base(element, automationIdBasedFinder)
        {
        }

        public void Close()
        {
            ((WindowPattern)Element.GetCurrentPattern(WindowPattern.Pattern))
                .Close();
        }

        public static Window FindFromDesktop(string windowName)
        {
            throw new NotImplementedException();
        }
    }
}