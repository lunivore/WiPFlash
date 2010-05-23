#region

using System;
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
            return Find<T>(_finder, componentName);
        }

        public T Find<T>(IFindAutomationElements finder, string name) where T : AutomationElementWrapper<T>
        {
            return finder.Find<T>(this, name);
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get {
                return new AutomationEventWrapper[] {new FocusEvent()};
            }
        }

        public bool Contains(string name)
        {
            return Contains(_finder, name);
        }

        public bool Contains(IFindAutomationElements finder, string name)
        {
            return finder.Contains(this, name);
        }
    }
}
