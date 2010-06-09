#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Exceptions;
using WiPFlash.Framework;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class Container<T> : AutomationElementWrapper<T>, IHandleFailureToFindChildren where T : Container<T>
    {
        private readonly IFindAutomationElements _finder;
        public FailureToFindHandler HandlerForFailingToFind { get; set; }

        public Container(AutomationElement element) : this(element, string.Empty)
        {
        }

        public Container(AutomationElement element, string name) : this(element, name, new NameBasedFinder(new WrapperFactory()))
        {
            
        }

        public Container(AutomationElement element, string name, IFindAutomationElements finder) : base(element, name)
        {
            _finder = finder;
            HandlerForFailingToFind = (s) => { throw new FailureToFindException(s); };
        }

        public TC Find<TC>(string componentName) where TC : AutomationElementWrapper<TC>
        {
            return Find<TC>(_finder, componentName);
        }

        public TC Find<TC>(IFindAutomationElements finder, object value) where TC : AutomationElementWrapper<TC>
        {
            TC find = finder.Find<TC, T>(this, value, HandlerForFailingToFind);
            if (find is IHandleFailureToFindChildren)
            {
                ((IHandleFailureToFindChildren) find).HandlerForFailingToFind = HandlerForFailingToFind;
            }
            return find;
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

        public bool Contains(IFindAutomationElements finder, object value)
        {
            return finder.Contains(this, value);
        }
    }
}
