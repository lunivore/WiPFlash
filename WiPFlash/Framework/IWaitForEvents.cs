using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Framework.Events;

namespace WiPFlash.Framework
{
    public delegate bool SomethingToWaitFor(AutomationElementWrapper source, AutomationEventArgs e);
    public delegate void FailureToHappenHandler(AutomationElementWrapper elementWrapper);

    public interface IWaitForEvents
    {
        bool WaitFor(AutomationElementWrapper wrapper, SomethingToWaitFor check, TimeSpan timeout, FailureToHappenHandler handler, IEnumerable<AutomationEventWrapper> enumerable);
    }
}