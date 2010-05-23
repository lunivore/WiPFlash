#region

using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public interface IFindAutomationElements
    {
        T Find<T>(Container root, string argument)
            where T : AutomationElementWrapper<T>;

        bool Contains(Container root, string argument);
    }
}