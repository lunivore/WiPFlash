using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class ListBox : AutomationElementWrapper
    {
        public ListBox(AutomationElement element) : base(element)
        {
        }

        public void Select(params string[] selections)
        {
            var selectionList = new List<string>(selections);
            foreach (AutomationElement listItem in Element.FindAll(TreeScope.Children,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem)))
            {
                if (selectionList.Contains(listItem.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString()))
                {
                    ((SelectionItemPattern)listItem.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();
                }
            }
        }
    }
}