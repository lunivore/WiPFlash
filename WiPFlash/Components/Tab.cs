using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class Tab : AutomationElementWrapper
    {
        public Tab(AutomationElement element) : base(element)
        {
        }

        public void Select()
        {
            ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();
        }

        public bool HasFocus()
        {
            return bool.Parse(Element.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty).ToString());
        }
    }
}
