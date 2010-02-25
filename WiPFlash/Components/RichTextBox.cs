using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;

namespace WiPFlash.Components
{
    public class RichTextBox : AutomationElementWrapper
    {
        public RichTextBox(AutomationElement element) : base(element)
        {
        }

        public string Text
        {
            get
            {
                return ((TextPattern) Element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.GetText(-1);
            }
            set
            {
                Element.SetFocus();
                ((TextPattern)Element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.Select();
                SendKeys.SendWait("{DEL}");
                SendKeys.SendWait(value);
            }
        }
    }
}
