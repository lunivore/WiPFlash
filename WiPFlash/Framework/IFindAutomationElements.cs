#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public delegate void FailureToFindHandler(string message);

    public interface IFindAutomationElements
    {
        T Find<T, TC>(Container<TC> root, PropertyCondition condition, FailureToFindHandler failureToFindHandler)
            where T : AutomationElementWrapper<T>
            where TC : Container<TC>;

        bool Contains<TC>(Container<TC> root, PropertyCondition condition)
            where TC : Container<TC>;
    }
}