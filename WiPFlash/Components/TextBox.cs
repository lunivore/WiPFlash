#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class TextBox : AutomationElementWrapper<TextBox>
    {
        public TextBox(AutomationElement element, string name) : base(element, name)
        {
        }

        public string Text
        {
            get { return ((ValuePattern)Element.GetCurrentPattern(ValuePattern.Pattern)).Current.Value; }
            set { ((ValuePattern) Element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(value); }
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                           {
                               new PropertyChangeEvent(TreeScope.Element, ValuePattern.ValueProperty)
                        };
            }

        }
    }
}