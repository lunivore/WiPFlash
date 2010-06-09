#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework
{
    public class NameBasedFinder : PropertyBasedFinder
    {
        public NameBasedFinder() : this(new WrapperFactory()) { }

        public NameBasedFinder(IWrapAutomationElements wrapper) : base(wrapper, AutomationElement.AutomationIdProperty)
        {
        }
    }
}