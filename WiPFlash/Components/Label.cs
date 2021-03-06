﻿#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class Label : AutomationElementWrapper
    {
        public Label(AutomationElement element, string name) : base(element, name)
        {
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                           {
                               new PropertyChangeEvent(TreeScope.Element, AutomationElement.NameProperty)
                        };
            }

        }

        public void Click()
        {
            throw new NotImplementedException();
        }
    }
}
