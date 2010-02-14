using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class Button : AutomationElementWrapper
    {
        public Button(AutomationElement element) : base(element)
        {
        }

        public void Click()
        {
            ((InvokePattern)Element.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        }
    }
}