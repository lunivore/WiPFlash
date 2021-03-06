﻿#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class Tab : Container
    {
        private readonly SelectionItemPatternWrapper _selectionItemPattern;

        public Tab(AutomationElement element, string name) : base(element, name)
        {
            _selectionItemPattern = new SelectionItemPatternWrapper(element);
        }

        public void Select()
        {
            _selectionItemPattern.Select();
        }

        public bool HasFocus()
        {
            return bool.Parse(Element.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty).ToString());
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get 
            {
                return new AutomationEventWrapper[] {new FocusEvent()};
            }
        }

        public string Title
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }

        public bool IsSelected()
        {
            return _selectionItemPattern.IsSelected();
        }
    }
}
