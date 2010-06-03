#region

using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public interface IFindAutomationElements
    {
        T Find<T, TC>(Container<TC> root, object argument)
            where T : AutomationElementWrapper<T>
            where TC : Container<TC>;

        bool Contains<TC>(Container<TC> root, object argument)
            where TC : Container<TC>;
    }
}