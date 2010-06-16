#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Framework
{
    public class PropertyBasedFinder : IFindAutomationElements
    {
        private readonly IWrapAutomationElements _wrapper;

        public PropertyBasedFinder() : this(new WrapperFactory()) {}

        public PropertyBasedFinder(IWrapAutomationElements wrapper)
        {
            _wrapper = wrapper;
        }

        public T Find<T, TC>(Container<TC> root, PropertyCondition condition, FailureToFindHandler failureToFindHandler) where T : AutomationElementWrapper<T> where TC : Container<TC>
        {
            AutomationElement element = root.Element.FindFirst(
                TreeScope.Descendants,
                condition);
            if (element == null)
            {
                failureToFindHandler(string.Format(
                     "Could not find an element with property '{0}', value {1} " +
                     "from the root starting with the element '{2}'. " +
                     "Please note that the Name in WPF classes maps to the AutomationIdProperty " +
                     "and the text or title is often a NameProperty.",
                     condition.Property.ProgrammaticName, condition.Value, 
                     root.Name));
                return null;
            }
            return _wrapper.Wrap<T>(element, string.Format("{0}[{1}]", condition.Property.ProgrammaticName, condition.Value));
        }

        public bool Contains<TC>(Container<TC> root, PropertyCondition condition) where TC : Container<TC>
        {
            return Find<ContainedElement, TC>(root, condition, (s) => { }) != null;
        }

        private class ContainedElement : AutomationElementWrapper<ContainedElement>
        {
            public ContainedElement(AutomationElement element, string name) : base(element, name)
            {
            }

            protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
            {
                get { return new AutomationEventWrapper[0]; }
            }
        }
    }
}