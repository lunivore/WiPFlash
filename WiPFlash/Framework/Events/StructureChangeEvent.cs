#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework.Events
{
    public class StructureChangeEvent : AutomationEventWrapper
    {
        private readonly TreeScope _scope;
        private StructureChangedEventHandler _handler;
        private AutomationElement _element;

        public StructureChangeEvent(TreeScope scope)
        {
            _scope = scope;
        }

        public override void Add(WrappedEventHandler handler, AutomationElementWrapper element)
        {
            _handler = (o, e) => handler(element, e);
            _element = element.Element;
            Automation.AddStructureChangedEventHandler(_element, _scope, _handler);
        }

        public override void Remove()
        {
            Automation.RemoveStructureChangedEventHandler(_element, _handler);
        }
    }
}