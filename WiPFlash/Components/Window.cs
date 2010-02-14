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
    }
}