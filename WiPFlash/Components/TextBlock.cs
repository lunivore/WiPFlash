using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class TextBlock : AutomationElementWrapper
    {
        public TextBlock(AutomationElement element) : base(element)
        {
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }
    }
}
