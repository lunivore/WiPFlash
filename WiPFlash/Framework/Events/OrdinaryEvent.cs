#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework.Events
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

        public override void Add(WrappedEventHandler handler, AutomationElementWrapper element)
        {
            _handler = (o, e) => handler(element, e);
            _element = element.Element;
            Automation.AddAutomationEventHandler(_eventId, _element, _scope, _handler);
        }

        public override void Remove()
        {
            Automation.RemoveAutomationEventHandler(_eventId, _element, _handler);
        }
    }
}