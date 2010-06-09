#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework
{
    public class TitleBasedFinder : PropertyBasedFinder
    {
        public TitleBasedFinder() : this(new WrapperFactory()) {}

        public TitleBasedFinder(IWrapAutomationElements wrapper) : base(wrapper, AutomationElement.NameProperty)
        {
        }
    }
}