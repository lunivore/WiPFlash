#region

using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class OrdinaryEvent : AutomationEventWrapper
    {
        private readonly AutomationEvent _eventId;
        private readonly TreeScope _scope;
        private AutomationEventHandler _handler;
        private AutomationElement _element;

        public OrdinaryEvent(AutomationEvent eventId, TreeScope scope)
        {
            _scope = scope;
            _eventId = eventId;
        }

        public override void Add(WrappedEventHandler handler, AutomationElement element)
        {
            _handler = (o, e) => handler();
            _element = element;
            Automation.AddAutomationEventHandler(_eventId, element, _scope, _handler);
        }

        public override void Remove()
        {
            Automation.RemoveAutomationEventHandler(_eventId, _element, _handler);
        }
    }
}