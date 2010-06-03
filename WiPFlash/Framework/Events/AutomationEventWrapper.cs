#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework.Events
{
    public delegate void WrappedEventHandler();
    public abstract class AutomationEventWrapper
    {
        public abstract void Add(WrappedEventHandler handler, AutomationElement element);
        public abstract void Remove();
    }
}