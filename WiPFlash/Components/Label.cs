using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class Label : AutomationElementWrapper
    {
        public Label(AutomationElement element) : base(element)
        {
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }
    }
}
