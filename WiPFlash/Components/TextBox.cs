#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class TextBox : AutomationElementWrapper
    {
        private readonly ValuePatternWrapper _valuePattern;

        public TextBox(AutomationElement element, string name) : base(element, name)
        {
            _valuePattern = new ValuePatternWrapper(element);
        }

        public string Text
        {
            get { return _valuePattern.Value; }
            set { _valuePattern.Value = value; }
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