#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public delegate void FailureToFindHandler(string message);

    public interface IFindAutomationElements
    {
        T Find<T>(Container root, Condition condition, FailureToFindHandler failureToFindHandler)
            where T : AutomationElementWrapper;

        bool Contains(Container root, Condition condition);

        IEnumerable<T> FindAll<T>(Container container, Condition condition, FailureToFindHandler handlerForFailingToFind)
            where T : AutomationElementWrapper;
    }
}