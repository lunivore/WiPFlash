using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using WiPFlash.Framework;

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
