#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class Tab : AutomationElementWrapper<Tab>
    {
        public Tab(AutomationElement element, string name) : base(element, name)
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

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get {
                return new AutomationEventWrapper[] {new FocusEvent()};
            }
        }
    }
}
