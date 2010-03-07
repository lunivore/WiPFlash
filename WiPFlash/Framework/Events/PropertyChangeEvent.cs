#region

using System.Windows.Automation;

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

        public override void Add(WrappedEventHandler handler, AutomationElement element)
        {
            _element = element;
            _handler = (o, e) => handler();
            Automation.AddAutomationPropertyChangedEventHandler(element, _scope, _handler, _properties);
        }

        public override void Remove()
        {
            Automation.RemoveAutomationPropertyChangedEventHandler(_element, _handler);
        }
    }
}
