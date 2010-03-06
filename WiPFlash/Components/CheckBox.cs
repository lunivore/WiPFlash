#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class CheckBox : AutomationElementWrapper
    {
        public CheckBox(AutomationElement element) : base(element)
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
    }
}
