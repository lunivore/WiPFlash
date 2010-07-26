using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class ListView : ListBox
    {
        public ListView(AutomationElement element, string name) : base(element, name)
        {
        }
    }
}
