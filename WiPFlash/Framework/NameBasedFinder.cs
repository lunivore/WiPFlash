#region

using System.Windows.Automation;
using WiPFlash.Components;

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

        public T Find<T>(AutomationElementWrapper root, string name) where T : AutomationElementWrapper
        {
            return _wrapper.Wrap<T>(root.Element.FindFirst(TreeScope.Descendants,
                                             new PropertyCondition(AutomationElement.AutomationIdProperty, name)));

        }
    }
}