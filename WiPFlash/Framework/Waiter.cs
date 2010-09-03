using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Components;
using WiPFlash.Framework.Events;

namespace WiPFlash.Framework
{
    public class Waiter : IWaitForEvents
    {
        private AutomationEventArgs _triggeringEvent;
        public Waiter()
        {
            _waitingRoom = new object();
        }

        private readonly object _waitingRoom;


        private void ClearPulsingHandlers(IEnumerable<AutomationEventWrapper> eventWrappers)
        {
            foreach (var wrapper in eventWrappers)
            {
                wrapper.Remove();
            }
        }

        private IEnumerable<AutomationEventWrapper> AddPulsingHandlers(IEnumerable<AutomationEventWrapper> eventWrappers, AutomationElementWrapper element)
        {
            foreach (var wrapper in eventWrappers)
            {
                wrapper.Add((src, e) =>
                {
                    _triggeringEvent = e;
                    PulseTheWaitingRoom();
                }, element);
            }
            return eventWrappers;
        }

        public bool WaitFor(AutomationElementWrapper element, SomethingToWaitFor check, TimeSpan timeout, FailureToHappenHandler failureHandler, IEnumerable<AutomationEventWrapper> events)
        {
            Monitor.Enter(_waitingRoom);
            _triggeringEvent = null;

            DateTime started = DateTime.Now;
            var handlerRemovers = AddPulsingHandlers(events, element);

            bool checkPassed = true;
            while (!check(element, _triggeringEvent) && DateTime.Now.Subtract(started).CompareTo(timeout) < 0)
            {
                checkPassed = false;
                Monitor.Wait(_waitingRoom, timeout);
            }
            Monitor.Exit(_waitingRoom);
            ClearPulsingHandlers(handlerRemovers);

            if (!checkPassed && !check(element, null))
            {
                failureHandler(element);
                return false;
            }
            return true;
        }


        private void PulseTheWaitingRoom()
        {
            Monitor.Enter(_waitingRoom);
            Monitor.Pulse(_waitingRoom);
            Monitor.Exit(_waitingRoom);
        }
    }
}
