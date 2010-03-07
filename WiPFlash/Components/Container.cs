#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class Container : AutomationElementWrapper<Container>
    {
        private readonly IFindAutomationElements _finder;

        public Container(AutomationElement element) : this(element, string.Empty)
        {
        }

        public Container(AutomationElement element, string name) : this(element, name, new NameBasedFinder(new WrapperFactory()))
        {
            
        }

        public Container(AutomationElement element, string name, IFindAutomationElements finder) : base(element, name)
        {
            _finder = finder;
        }

        public T Find<T>(string componentName) where T : AutomationElementWrapper<T>
        {
            return _finder.Find<T>(this, componentName);            
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get {
                return new AutomationEventWrapper[] {new FocusEvent()};
            }
        }
    }
}
