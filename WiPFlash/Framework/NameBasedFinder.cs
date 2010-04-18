#region

using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Exceptions;

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