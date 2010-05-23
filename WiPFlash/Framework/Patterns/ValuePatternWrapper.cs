using System;
using System.Windows.Automation;

namespace WiPFlash.Framework.Patterns
{
    public class ValuePatternWrapper
    {
        public ValuePatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public string Value
        {
            get { return ((ValuePattern) Element.GetCurrentPattern(ValuePattern.Pattern)).Current.Value; }
            set { ((ValuePattern) Element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(value); }
        }

        public AutomationElement Element { get; set; }
    }
}