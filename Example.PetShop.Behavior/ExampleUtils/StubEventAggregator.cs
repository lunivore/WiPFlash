using System;
using System.Collections.Generic;
using Microsoft.Practices.Composite.Events;

namespace Example.PetShop.Behavior.ExampleUtils
{
    public class StubEventAggregator : IEventAggregator
    {
        private readonly IDictionary<object, object> _eventsByPayload = new Dictionary<object, object>();

        public TEventType GetEvent<TEventType>() where TEventType : EventBase
        {
            if (!_eventsByPayload.ContainsKey(typeof(TEventType)))
            {
                _eventsByPayload[typeof (TEventType)] = typeof(TEventType).GetConstructor(new Type[]{}).Invoke(new object[]{});
            }
            return (TEventType)_eventsByPayload[typeof(TEventType)];
        }
    }
}
