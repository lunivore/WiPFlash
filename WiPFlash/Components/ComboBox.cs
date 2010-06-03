#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Exceptions;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class ComboBox : AutomationElementWrapper<ComboBox>
    {
        private readonly SelectionPatternWrapper _selectionPattern;
        private readonly ExpandCollapsePatternWrapper _expandCollapsePattern;

        public ComboBox(AutomationElement element, string name) : base(element, name)
        {
            _selectionPattern = new SelectionPatternWrapper(element);
            _expandCollapsePattern = new ExpandCollapsePatternWrapper(element);
        }

        public void Select(string selection)
        {
            if (selection.Equals(string.Empty))
            {
                foreach (AutomationElement element in _selectionPattern.GetSelection())
                {
                    new SelectionItemPatternWrapper(element).RemoveFromSelection();
                }
            }
            else
            {
                bool found = false;
                _expandCollapsePattern.Expand();

                foreach (AutomationElement listItem in AllItemElements())
                {
                    if (listItem.GetCurrentPropertyValue(AutomationElement.NameProperty).Equals(selection))
                    {
                        new SelectionItemPatternWrapper(listItem).Select();                        
                        found = true;
                        break;
                    }
                }
                _expandCollapsePattern.Collapse();
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
            get { return _selectionPattern.Selection; }
        }

        public string[] Items
        {
            get {
                var result = new List<string>();
                _expandCollapsePattern.Expand();

                foreach (AutomationElement element in AllItemElements())
                {
                    result.Add(element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString());
                }
                _expandCollapsePattern.Collapse();

                return result.ToArray();
            }
        }

        public new void WaitFor(SomethingToWaitFor check)
        {
            _expandCollapsePattern.Expand();
            base.WaitFor(check);
            _expandCollapsePattern.Collapse();
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