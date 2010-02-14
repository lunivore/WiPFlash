using System.Windows.Automation;
using WiPFlash.Components;

namespace WiPFlash.Framework
{
    public interface IWrapAutomationElements
    {
        T Wrap<T>(AutomationElement element) where T : AutomationElementWrapper;
    }
}