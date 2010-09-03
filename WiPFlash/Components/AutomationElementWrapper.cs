#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using WiPFlash.Framework;
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

        private readonly AutomationElement _element;
        private readonly string _name;
        private readonly IWaitForEvents _waiter;

        protected AutomationElementWrapper(AutomationElement element, string name) : this(element, name, new Waiter())
        {
        }

        protected AutomationElementWrapper(AutomationElement element, string name, IWaitForEvents waiter)
        {
            if (element == null)
            {
                throw new NullReferenceException("No element was found for this " + GetType().Name);
            }
            _element = element;
            _name = name;
            _waiter = waiter;
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
            return _waiter.WaitFor(this, check, timeout, failureHandler, events);
        }

        protected abstract IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor { get; }

    }
}