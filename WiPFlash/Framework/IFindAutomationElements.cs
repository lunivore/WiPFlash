#region

using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public interface IFindAutomationElements
    {
        T Find<T>(Container root, string automationId)
            where T : AutomationElementWrapper<T>;
    }
}