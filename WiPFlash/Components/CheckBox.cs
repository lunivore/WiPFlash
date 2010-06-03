#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class CheckBox : AutomationElementWrapper<CheckBox>
    {
        private readonly TogglePatternWrapper _togglePattern;

        public CheckBox(AutomationElement element, string name) : base(element, name)
        {
            _togglePattern = new TogglePatternWrapper(element);
        }

        public bool Selected
        {
            get
            {
                return _togglePattern.Selected;
            }
            set
            {
                _togglePattern.Selected = value;
            }
        }

        public void Toggle()
        {
            _togglePattern.Toggle();
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get { return new AutomationEventWrapper[] {new PropertyChangeEvent(TreeScope.Element, TogglePattern.ToggleStateProperty) }; }
        }
    }
}
