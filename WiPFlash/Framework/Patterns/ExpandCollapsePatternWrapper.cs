#region

using System.Windows.Automation;

#endregion

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

        public bool IsAvailable()
        {
            return (bool) Element.GetCurrentPropertyValue(AutomationElement.IsExpandCollapsePatternAvailableProperty);
        }
    }
}