using System.Windows.Automation;
using WiPFlash.Framework;

namespace WiPFlash.Components
{
    public class Container : AutomationElementWrapper
    {
        private readonly IFindAutomationElements _finder;

        public Container(AutomationElement element) : this(element, new NameBasedFinder(new WrapperFactory()))
        {
        }

        public Container(AutomationElement element, IFindAutomationElements finder) : base(element)
        {
            _finder = finder;
        }

        public T Find<T>(string componentName) where T : AutomationElementWrapper
        {
            return _finder.Find<T>(this, componentName);            
        }
    }
}
