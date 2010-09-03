#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class Button : AutomationElementWrapper
    {
        private readonly InvokePatternWrapper _invokePattern;

        public Button(AutomationElement element, string name) : base(element, name)
        {
            _invokePattern = new InvokePatternWrapper(element);
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }

        public void Click()
        {
            _invokePattern.Invoke();
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

        public bool IsEnabled
        {
            get { return (Boolean)Element.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty); }
        }
    }
}