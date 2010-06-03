using System;
using System.Windows.Automation;

namespace WiPFlash.Framework.Patterns
{
    public class ExpandCollapsePatternWrapper
    {
        public ExpandCollapsePatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public AutomationElement Element { get; set; }

        public void Expand()
        {
            ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Expand();
        }

        public void Collapse()
        {
            ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Collapse();
        }
    }
}