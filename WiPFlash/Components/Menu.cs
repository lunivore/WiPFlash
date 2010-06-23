using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Windows.Controls;
using WiPFlash.Exceptions;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

namespace WiPFlash.Components
{
    public class Menu : Container<Menu>
    {
        private ExpandCollapsePatternWrapper _expandCollapsePattern;

        public Menu(AutomationElement element, string name) : base(element, name)
        {
            _expandCollapsePattern = new ExpandCollapsePatternWrapper(element);
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                   {
                       new StructureChangeEvent(TreeScope.Element),
                       new OrdinaryEvent(SelectionItemPattern.ElementSelectedEvent, TreeScope.Descendants)
                   };
            }
        }

        public string[] Items
        {
            get
            {
                if (_expandCollapsePattern.IsAvailable())
                {
                    _expandCollapsePattern.Expand();
                }
                var result = new List<string>();
                foreach (AutomationElement element in AllItemElements())
                {
                    result.Add(element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString());
                }
                if (_expandCollapsePattern.IsAvailable())
                {
                    _expandCollapsePattern.Collapse();
                }
                return result.ToArray();
            }
        }

        public new bool WaitFor(SomethingToWaitFor check, FailureToHappenHandler failureHandler)
        {
            var result = base.WaitFor(check, failureHandler);
            return result;
        }

        private IEnumerable AllItemElements()
        {
            return (Element.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem)));
        }

        public void Select(string item)
        {
            bool found = false;
            if (_expandCollapsePattern.IsAvailable()) { _expandCollapsePattern.Expand(); }
            foreach (AutomationElement menuItem in AllItemElements())
            {
                if (menuItem.GetCurrentPropertyValue(AutomationElement.NameProperty).Equals(item))
                {
                    new InvokePatternWrapper(menuItem).Invoke();
                    found = true;
                    break;
                }
            }
            if (_expandCollapsePattern.IsAvailable()) { _expandCollapsePattern.Collapse();}
            if (!found)
            {
                throw new FailureToFindException("Failed to find an element in this Menu with a value of " + item +
                    ". Please use the ToString() value of the element for selection");
            }
        }
    }
}
