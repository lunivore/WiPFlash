#region

using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public interface IFindAutomationElements
    {
        T Find<T>(AutomationElementWrapper wrapper, string automationId) where T : AutomationElementWrapper;        
    }
}