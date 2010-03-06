#region

using System.Windows.Automation;

#endregion

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
