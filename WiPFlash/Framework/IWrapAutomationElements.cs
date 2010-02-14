#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public interface IWrapAutomationElements
    {
        T Wrap<T>(AutomationElement element) where T : AutomationElementWrapper;
    }
}