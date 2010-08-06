#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    /**
     * Constructs an automation element wrapper which will find other elements by automation id (WPF element Name)
     */

    public abstract class AutomationElementWrapper
    {
        public TimeSpan DefaultWaitTimeout = TimeSpan.Parse("00:00:05");

        public delegate bool SomethingToWaitFor(AutomationElementWrapper source, AutomationEventArgs e);
        public delegate void FailureToHappenHandler(AutomationElementWrapper elementWrapper);

        protected delegate void WrappedEventHandler();

        private readonly AutomationElement _element;
        private readonly object _waitingRoom;
        private readonly string _name;
        private AutomationEventArgs _triggeringEvent;

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

        public bool IsOffscreen
        {
            get { return (bool) _element.GetCurrentPropertyValue(AutomationElement.IsOffscreenProperty); }
        }

        public AutomationElement Element
        {
            get { return _element; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool WaitFor(SomethingToWaitFor check, FailureToHappenHandler failureHandler)
        {
            return WaitFor(check, DefaultWaitTimeout, failureHandler);            
        }

        public bool WaitFor(SomethingToWaitFor check, TimeSpan timeout, FailureToHappenHandler failureHandler)
        {
            return WaitFor(check, timeout, failureHandler, SensibleEventsToWaitFor);
        }

        public bool WaitFor(SomethingToWaitFor check, TimeSpan timeout, FailureToHappenHandler failureHandler, IEnumerable<AutomationEventWrapper> events)
        {
            Monitor.Enter(_waitingRoom);
            _triggeringEvent = null;

            DateTime started = DateTime.Now;
            var handlerRemovers = AddPulsingHandlers(events);

            while(!check(this, _triggeringEvent) && DateTime.Now.Subtract(started).CompareTo(timeout) < 0)
            {
                Monitor.Wait(_waitingRoom, timeout);
            }
            Monitor.Exit(_waitingRoom);
            ClearPulsingHandlers(handlerRemovers);

            if (!check(this, null))
            {
                failureHandler(this);
                return false;
            }
            return true;
        }

        private void ClearPulsingHandlers(IEnumerable<AutomationEventWrapper> eventWrappers)
        {
            foreach (var wrapper in eventWrappers)
            {
                wrapper.Remove();
            }
        }

        private IEnumerable<AutomationEventWrapper> AddPulsingHandlers(IEnumerable<AutomationEventWrapper> eventWrappers)
        {
            foreach (var wrapper in eventWrappers)
            {
                wrapper.Add((src, e) =>
                                {
                                    _triggeringEvent = e;
                                    PulseTheWaitingRoom();
                                }, this);
            }
            return eventWrappers;
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