using System;
using System.Windows.Automation;

namespace WiPFlash.Framework.Patterns
{
    public class TogglePatternWrapper
    {
        public AutomationElement Element { get; set; }

        public bool Selected
        {
            get
            {
                ToggleState state = ((TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern)).Current.ToggleState;
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

        public TogglePatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public void Toggle()
        {
            ((TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern)).Toggle();
        }
    }
}