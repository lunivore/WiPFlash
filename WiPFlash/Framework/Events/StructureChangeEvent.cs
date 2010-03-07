#region

using System.Windows.Automation;

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

        public override void Add(WrappedEventHandler handler, AutomationElement element)
        {
            _handler = (o, e) => handler();
            _element = element;
            Automation.AddStructureChangedEventHandler(element, _scope, _handler);
        }

        public override void Remove()
        {
            Automation.RemoveStructureChangedEventHandler(_element, _handler);
        }
    }
}