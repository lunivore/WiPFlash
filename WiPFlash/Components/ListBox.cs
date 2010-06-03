#region

using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class ListBox : AutomationElementWrapper<ListBox>
    {
        private readonly SelectionPatternWrapper _selectionPattern;

        public ListBox(AutomationElement element, string name) : base(element, name)
        {
            _selectionPattern = new SelectionPatternWrapper(element);
        }

        public string[] Selection
        {
            get
            {
                var selectedItems = GetSelectedElements();
                var result = new List<string>();
                foreach (AutomationElement item in selectedItems)
                {
                    result.Add(item.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString());
                }
                return result.ToArray();
            }
        }

        public string[] Items
        {
            get
            {
                var result = new List<string>();
                foreach (AutomationElement element in AllItemElements())
                {
                    result.Add(element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString());
                }
                return result.ToArray();
            }
        }

        private IEnumerable GetSelectedElements()
        {
            return _selectionPattern.GetSelection();
        }

        public void Select(params string[] selections)
        {
            var selectionList = new List<string>(selections);
            foreach (AutomationElement listItem in AllItemElements())
            {
                if (selectionList.Contains(listItem.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString()))
                {
                    new SelectionItemPatternWrapper(listItem).AddToSelection();
                }
            }
        }

        private AutomationElementCollection AllItemElements()
        {
            return Element.FindAll(TreeScope.Children,
                                   new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
        }

        public void ClearSelection()
        {
            var selection = GetSelectedElements();
            foreach (AutomationElement element in selection)
            {
                new SelectionItemPatternWrapper(element).RemoveFromSelection();
            }
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get 
            {
                return new AutomationEventWrapper[]
                   {
                       new StructureChangeEvent(TreeScope.Element),
                       new OrdinaryEvent(SelectionItemPattern.ElementAddedToSelectionEvent, TreeScope.Descendants),
                       new OrdinaryEvent(SelectionItemPattern.ElementSelectedEvent, TreeScope.Descendants),
                       new OrdinaryEvent(SelectionItemPattern.ElementRemovedFromSelectionEvent, TreeScope.Descendants)
                   };
            }
        }
    }
}