#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Exceptions;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    /**
     * Constructs an automation element wrapper which will find other elements by automation id (WPF element Name)
     */

    public abstract class AutomationElementWrapper<T> where T : AutomationElementWrapper<T>
    {
        public TimeSpan DEFAULT_WAIT_TIMEOUT = TimeSpan.Parse("00:00:05");

        public delegate bool SomethingToWaitFor(T elementWrapper);
        protected delegate void WrappedEventHandler();

        private readonly AutomationElement _element;
        private readonly object _waitingRoom;
        private readonly string _name;

        protected AutomationElementWrapper(AutomationElement element) : this(element, "")
        {
        }

        protected AutomationElementWrapper(AutomationElement element, string name)
        {
            if (element == null)
            {
                throw new NullReferenceException("No element was found for this " + GetType().Name);
            }
            _element = element;
            _name = name;
            _waitingRoom = new object();
        }

        public AutomationElement Element
        {
            get { return _element; }
        }

        public string Name
        {
            get { return _name; }
        }

        public void WaitFor(SomethingToWaitFor check)
        {
            WaitFor(check, DEFAULT_WAIT_TIMEOUT);            
        }
        
        private void WaitFor(SomethingToWaitFor check, TimeSpan timeout)
        {
            Monitor.Enter(_waitingRoom);
            DateTime started = DateTime.Now;
            var handlerRemovers = AddPulsingHandlers();

            while(!check((T)this) && DateTime.Now.Subtract(started).CompareTo(timeout) < 0)
            {
                Monitor.Wait(_waitingRoom, timeout);
            }
            Monitor.Exit(_waitingRoom);
            ClearPulsingHandlers(handlerRemovers);

            if (!check((T)this))
            {
                throw new FailureToHappenException(
                    String.Format("Element {0} of type {1} failed to meet your criteria in time",
                    _name, GetType().Name));
            }
        }

        private void ClearPulsingHandlers(IEnumerable<AutomationEventWrapper> eventWrappers)
        {
            foreach (var wrapper in eventWrappers)
            {
                wrapper.Remove();
            }
        }

        private IEnumerable<AutomationEventWrapper> AddPulsingHandlers()
        {
            var wirers = SensibleEventsToWaitFor;
            foreach (var wirer in wirers)
            {
                wirer.Add(PulseTheWaitingRoom, Element);
            }
            return wirers;
        }

        protected abstract IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor { get; }

        private void PulseTheWaitingRoom()
        {
            Monitor.Enter(_waitingRoom);
            Monitor.Pulse(_waitingRoom);
            Monitor.Exit(_waitingRoom);
        }
    }
}