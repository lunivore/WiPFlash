#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Exceptions;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class ComboBox : AutomationElementWrapper<ComboBox>
    {
        public ComboBox(AutomationElement element, string name) : base(element, name)
        {
        }

        public void Select(string selection)
        {
            if (selection.Equals(string.Empty))
            {
                foreach (var element in ((SelectionPattern)Element.GetCurrentPattern(SelectionPattern.Pattern)).Current.GetSelection())
                {
                    ((SelectionItemPattern)element.GetCurrentPattern(SelectionItemPattern.Pattern)).RemoveFromSelection();
                }
            }
            else
            {
                bool found = false;
                ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Expand();

                foreach (AutomationElement listItem in AllItemElements())
                {
                    if (listItem.GetCurrentPropertyValue(AutomationElement.NameProperty).Equals(selection))
                    {
                        ((SelectionItemPattern)listItem.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();                        
                        found = true;
                        break;
                    }
                }
                ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Collapse();
                if (!found)
                {
                    throw new FailureToFindException("Failed to find an element in this ComboBox with a value of " + selection + 
                        ". Please use the ToString() value of the element for selection, or string.Empty to clear the selection. If you want " +
                        "to select arbitrary text in an editable ComboBox, please use the EditableComboBox.Text method.");
                }
            }
        }

        private AutomationElementCollection AllItemElements()
        {
            return (Element.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem)));
        }

        public string Selection
        {
            get
            {
                AutomationElement[] selections =
                    ((SelectionPattern) Element.GetCurrentPattern(SelectionPattern.Pattern)).Current.GetSelection();
                if (selections.Length < 1) 
                {
                    return ""; 
                }
                return selections[0].GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
            }
        }

        public string[] Items
        {
            get {
                var result = new List<string>();
                ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Expand();

                foreach (AutomationElement element in AllItemElements())
                {
                    result.Add(element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString());
                }
                ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Collapse();

                return result.ToArray();
            }
        }

        public new void WaitFor(SomethingToWaitFor check)
        {
            ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Expand();
            base.WaitFor(check);
            ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Collapse();
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
    }
}