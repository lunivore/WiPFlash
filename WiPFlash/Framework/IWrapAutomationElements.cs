#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public interface IWrapAutomationElements
    {
        T Wrap<T>(AutomationElement element, string name) where T : AutomationElementWrapper;
        T Wrap<T>(AutomationElement element, PropertyCondition condition) where T : AutomationElementWrapper;
    }
}