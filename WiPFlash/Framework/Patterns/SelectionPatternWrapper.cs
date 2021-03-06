#region

using System.Collections;
using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework.Patterns
{
    public class SelectionPatternWrapper
    {
        public SelectionPatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public AutomationElement Element { get; set; }

        public string Selection
        {
            get
            {
                AutomationElement[] selections =
                    ((SelectionPattern)Element.GetCurrentPattern(SelectionPattern.Pattern)).Current.GetSelection();
                if (selections.Length < 1)
                {
                    return "";
                }
                return selections[0].GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
            }
        }

        public IEnumerable GetSelection()
        {
            return ((SelectionPattern) Element.GetCurrentPattern(SelectionPattern.Pattern)).Current.GetSelection();
        }

        public AutomationElementCollection ItemElements()
        {
            return Element.FindAll(TreeScope.Children,
                            new PropertyCondition(AutomationElement.IsSelectionItemPatternAvailableProperty, true));
        }
    }
}