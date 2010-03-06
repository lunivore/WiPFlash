using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class RadioButton : AutomationElementWrapper
    {
        public RadioButton(AutomationElement element) : base(element)
        {
        }

        public bool Selected
        {
            get { return bool.Parse(((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).Current.IsSelected.ToString()); }
        }

        public void Select()
        {
            var pattern = ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern));
            pattern.Select();
            
        }
    }
}
