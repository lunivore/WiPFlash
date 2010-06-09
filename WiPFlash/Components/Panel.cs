#region

using System.Windows.Automation;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Components
{
    public class Panel : Container<Panel>
    {
        public Panel(AutomationElement element, string name) : base(element, name)
        {
        }

        public Panel(AutomationElement element, string name, IFindAutomationElements finder) : base(element, name, finder)
        {
        }
    }
}
