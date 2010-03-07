#region

using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Exceptions;

#endregion

namespace WiPFlash.Framework
{
    public class NameBasedFinder : IFindAutomationElements
    {
        private IWrapAutomationElements _wrapper;

        public NameBasedFinder(IWrapAutomationElements wrapper)
        {
            _wrapper = wrapper;
        }

        public T Find<T>(Container root, string name) 
            where T : AutomationElementWrapper<T>
        {
            AutomationElement element = root.Element.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, name));
            if (element == null)
            {
                throw new FailureToFindException(string.Format(
                    "Could not find an element called '{0}'" +
                    " from the root starting with the element '{1}'. This should be the Name on your WPF class, " +
                    "mapping to the AutomationId in Microsoft's UI automation.", 
                    name, root.Element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty)));
            }
            return _wrapper.Wrap<T>(element, name);
        }
    }
}