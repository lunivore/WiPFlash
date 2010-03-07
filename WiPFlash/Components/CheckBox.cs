#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class CheckBox : AutomationElementWrapper<CheckBox>
    {
        public CheckBox(AutomationElement element, string name) : base(element, name)
        {
        }

        public bool Selected
        {
            get
            {
                ToggleState state = ((TogglePattern) Element.GetCurrentPattern(TogglePattern.Pattern)).Current.ToggleState;
                return state.Equals(ToggleState.On);
            }
            set
            {
                if (Selected && !value || !Selected && value)
                {
                    Toggle();
                }
            }
        }

        public void Toggle()
        {
            ((TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern)).Toggle();
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get { return new AutomationEventWrapper[] {new PropertyChangeEvent(TreeScope.Element, TogglePattern.ToggleStateProperty) }; }
        }
    }
}
