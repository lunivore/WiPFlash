#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework.Events
{
    public class PropertyChangeEvent : AutomationEventWrapper
    {
        private readonly TreeScope _scope;
        private readonly AutomationProperty[] _properties;
        private AutomationPropertyChangedEventHandler _handler;
        private AutomationElement _element;

        public PropertyChangeEvent(TreeScope scope, params AutomationProperty[] properties)
        {
            _scope = scope;
            _properties = properties;
        }

        public override void Add(WrappedEventHandler handler, AutomationElementWrapper element)
        {
            _element = element.Element;
            _handler = (o, e) => handler(element, e);
            Automation.AddAutomationPropertyChangedEventHandler(_element, _scope, _handler, _properties);
        }

        public override void Remove()
        {
            Automation.RemoveAutomationPropertyChangedEventHandler(_element, _handler);
        }
    }
}
