#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework.Events
{
    public class FocusEvent : AutomationEventWrapper
    {
        private AutomationFocusChangedEventHandler _handler;

        public override void Add(WrappedEventHandler handler, AutomationElementWrapper element)
        {
            _handler = (o, e) => handler(element, e);
            Automation.AddAutomationFocusChangedEventHandler(_handler);
        }

        public override void Remove()
        {
            Automation.RemoveAutomationFocusChangedEventHandler(_handler);
        }
    }
}