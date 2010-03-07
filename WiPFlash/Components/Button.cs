#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class Button : AutomationElementWrapper<Button>
    {
        public Button(AutomationElement element, string name) : base(element, name)
        {
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }

        public void Click()
        {
            ((InvokePattern)Element.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                           {
                               new PropertyChangeEvent(TreeScope.Element, 
                                   AutomationElement.NameProperty, 
                                   AutomationElement.IsEnabledProperty)
                           };
            }
        }
    }
}