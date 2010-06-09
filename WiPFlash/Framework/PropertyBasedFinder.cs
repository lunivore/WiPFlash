#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public class PropertyBasedFinder : IFindAutomationElements
    {
        private readonly IWrapAutomationElements _wrapper;
        private readonly AutomationProperty _property;

        public PropertyBasedFinder(AutomationProperty property) : this(new WrapperFactory(), property) {}

        public PropertyBasedFinder(IWrapAutomationElements wrapper, AutomationProperty property)
        {
            _wrapper = wrapper;
            _property = property;
        }

        public T Find<T, TC>(Container<TC> root, object argument, FailureToFindHandler failureToFindHandler) where T : AutomationElementWrapper<T> where TC : Container<TC>
        {
            AutomationElement element = root.Element.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(_property, argument));
            if (element == null)
            {
                failureToFindHandler(string.Format(
                     "Could not find an element called '{0}' " +
                     "from the root starting with the element '{1}'. " +
                     "This should be the Name on your WPF class, " +
                     "mapping to the AutomationId in Microsoft's UI automation.",
                     argument, root.Element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty)));
                return null;
            }
            return _wrapper.Wrap<T>(element, argument.ToString());
        }

        public bool Contains<TC>(Container<TC> root, object argument) where TC : Container<TC>
        {
            AutomationElement element = root.Element.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(_property, argument.ToString()));
            return element != null;
        }
    }
}