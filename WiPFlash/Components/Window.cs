#region

using System;
using System.Windows.Automation;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Components
{
    public class Window : Container
    {
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