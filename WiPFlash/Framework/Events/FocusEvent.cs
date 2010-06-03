#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework.Events
{
    public class FocusEvent : AutomationEventWrapper
    {
        private AutomationFocusChangedEventHandler _handler;

        public FocusEvent()
        {
        }

        public override void Add(WrappedEventHandler handler, AutomationElement element)
        {
            _handler = (o, e) => handler();
            Automation.AddAutomationFocusChangedEventHandler(_handler);
        }

        public override void Remove()
        {
            Automation.RemoveAutomationFocusChangedEventHandler(_handler);
        }
    }
}