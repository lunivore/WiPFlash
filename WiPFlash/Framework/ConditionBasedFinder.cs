#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Framework
{
    public class ConditionBasedFinder : IFindAutomationElements
    {
        private readonly IWrapAutomationElements _wrapper;
        private readonly IDescribeConditions _conditionDescriber;

        public ConditionBasedFinder() : this(new WrapperFactory(), new ConditionDescriber()) {}

        public ConditionBasedFinder(IWrapAutomationElements wrapper, IDescribeConditions conditionDescriber)
        {
            _wrapper = wrapper;
            _conditionDescriber = conditionDescriber;
        }

        public T Find<T>(Container root, Condition condition, FailureToFindHandler failureToFindHandler) where T : AutomationElementWrapper
        {
            AutomationElement element = root.Element.FindFirst(
                TreeScope.Descendants,
                condition);
            if (element == null)
            {
                failureToFindHandler(string.Format(
                     "Could not find an element: {0} " + Environment.NewLine + " from within element: {1} ",
                     _conditionDescriber.Describe(condition), 
                     root.Name));
                return null;
            }
            return _wrapper.Wrap<T>(element, _conditionDescriber.Describe(condition));
        }

        public bool Contains(Container root, Condition condition)
        {
            return Find<ContainedElement>(root, condition, s => { }) != null;
        }

        private class ContainedElement : AutomationElementWrapper
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