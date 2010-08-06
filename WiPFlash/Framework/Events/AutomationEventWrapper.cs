#region

using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework.Events
{
    public delegate void WrappedEventHandler(AutomationElementWrapper source, AutomationEventArgs e);
    public abstract class AutomationEventWrapper
    {
        public abstract void Add(WrappedEventHandler handler, AutomationElementWrapper element);
        public abstract void Remove();
    }
}