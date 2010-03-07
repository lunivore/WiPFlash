#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class RadioButton : AutomationElementWrapper<RadioButton>
    {
        public RadioButton(AutomationElement element, string name) : base(element, name)
        {
        }

        public bool Selected
        {
            get { return bool.Parse(((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).Current.IsSelected.ToString()); }
        }

        public void Select()
        {
            var pattern = ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern));
            pattern.Select();
            
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                           {
                                new OrdinaryEvent(SelectionItemPattern.ElementSelectedEvent, TreeScope.Element)
                           };
            }
        }
    }
}
