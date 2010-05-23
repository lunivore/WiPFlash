using System;
using System.Windows.Automation;

namespace WiPFlash.Framework.Patterns
{
    public class SelectionItemPatternWrapper
    {
        public AutomationElement Element { get; set; }

        public bool Selected
        {
            get 
            {
                return bool.Parse(
                    ((SelectionItemPattern) Element.GetCurrentPattern(SelectionItemPattern.Pattern)).Current.IsSelected.
                        ToString()); 
            }
        }

        public SelectionItemPatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public void Select()
        {
            ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();
        }

        public void RemoveFromSelection()
        {
            ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).RemoveFromSelection();
        }

        public void AddToSelection()
        {
            ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).AddToSelection();
        }
    }
}