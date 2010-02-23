#region

using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class ComboBox : AutomationElementWrapper
    {
        public ComboBox(AutomationElement element) : base(element)
        {
        }

        public void Select(string selection)
        {
            if (Editable)
            {
                ((ValuePattern)Element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(selection);
            } else
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
                    ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Expand();
                    /********************************************************************************************/
                    /* Non-editable combo boxes require you to override "toString" in their underlying objects. */
                    /********************************************************************************************/
                    foreach (AutomationElement listItem in (Element.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem))))
                    {
                        if (listItem.GetCurrentPropertyValue(AutomationElement.NameProperty).Equals(selection))
                        {
                            ((SelectionItemPattern)listItem.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();
                            Thread.Sleep(100);
                            break;
                        }
                    }                    
                    ((ExpandCollapsePattern)Element.GetCurrentPattern(ExpandCollapsePattern.Pattern)).Collapse();
                }
            }
        }

        protected bool Editable
        {
            get
            {
                return new List<AutomationPattern>(Element.GetSupportedPatterns()).Contains(ValuePattern.Pattern);
            }
        }

        public string Selection
        {
            get
            {
                if (Editable)
                {
                    return ((ValuePattern) Element.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;
                }
                AutomationElement[] selections =
                    ((SelectionPattern) Element.GetCurrentPattern(SelectionPattern.Pattern)).Current.GetSelection();
                if (selections.Length < 1) 
                {
                    return ""; 
                }
                return selections[0].GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
            }
        }
    }
}