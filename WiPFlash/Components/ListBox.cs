#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class ListBox : AutomationElementWrapper
    {
        public ListBox(AutomationElement element) : base(element)
        {
        }

        public string[] Selection
        {
            get
            {
                var selectedItems = GetSelectedElements();
                var result = new List<string>();
                foreach (var item in selectedItems)
                {
                    result.Add(item.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString());
                }
                return result.ToArray();
            }
        }

        private AutomationElement[] GetSelectedElements()
        {
            return ((SelectionPattern) Element.GetCurrentPattern(SelectionPattern.Pattern))
                .Current.GetSelection();
        }

        public void Select(params string[] selections)
        {
            var selectionList = new List<string>(selections);
            foreach (AutomationElement listItem in Element.FindAll(TreeScope.Children,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem)))
            {
                if (selectionList.Contains(listItem.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString()))
                {
                    ((SelectionItemPattern)listItem.GetCurrentPattern(SelectionItemPattern.Pattern)).AddToSelection();
                }
            }
        }

        public void ClearSelection()
        {
            var selection = GetSelectedElements();
            foreach (var element in selection)
            {
                ((SelectionItemPattern)element.GetCurrentPattern(SelectionItemPattern.Pattern))
                    .RemoveFromSelection();
            }
        }
    }
}