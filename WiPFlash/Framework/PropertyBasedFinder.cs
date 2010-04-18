using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Exceptions;

namespace WiPFlash.Framework
{
    public abstract class PropertyBasedFinder : IFindAutomationElements
    {
        private readonly IWrapAutomationElements _wrapper;
        private readonly AutomationProperty _property;

        protected PropertyBasedFinder(IWrapAutomationElements wrapper, AutomationProperty property)
        {
            _wrapper = wrapper;
            _property = property;
        }

        public T Find<T>(Container root, string argument) where T : AutomationElementWrapper<T>
        {
            AutomationElement element = root.Element.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(_property, argument));
            if (element == null)
            {
                throw new FailureToFindException(string.Format(
                                                     "Could not find an element called '{0}' " +
                                                     "from the root starting with the element '{1}'. " +
                                                     "This should be the Name on your WPF class, " +
                                                     "mapping to the AutomationId in Microsoft's UI automation.",
                                                     argument, root.Element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty)));
            }
            return _wrapper.Wrap<T>(element, argument);
        }
    }
}