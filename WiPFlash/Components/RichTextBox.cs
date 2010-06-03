#region

using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Components
{
    public class RichTextBox : AutomationElementWrapper<RichTextBox>
    {
        public RichTextBox(AutomationElement element, string name) : base(element, name)
        {
            
        }

        public string Text
        {
            get
            {
                return RetrieveTextFrom(Element);
            }
            set
            {
                Element.SetFocus();
                ((TextPattern)Element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.Select();
                SendKeys.SendWait("{DEL}");
                SendKeys.SendWait(value);
            }
        }

        private string RetrieveTextFrom(AutomationElement element)
        {
            string text = string.Empty;
            bool isALabel = element.GetCurrentPropertyValue(AutomationElement.ClassNameProperty).Equals(typeof(System.Windows.Controls.Label).Name);
            bool isATextBlock = element.GetCurrentPropertyValue(AutomationElement.ClassNameProperty).Equals(typeof(System.Windows.Controls.TextBlock).Name);
            if (element.GetSupportedPatterns().Contains(TextPattern.Pattern))
            {
                TextPatternRange document = ((TextPattern) element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange;
                text = document.GetText(-1);
                AutomationElement[] children = document.GetChildren();
                foreach (var child in children)
                {
                    text = text + RetrieveTextFrom(child);
                }
            } else if (element.GetSupportedPatterns().Contains(ValuePattern.Pattern))
            {
                text = ((ValuePattern) element.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;
            } else if (isALabel || isATextBlock) {
                text = element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
            }
            return text;
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                           {
                                new StructureChangeEvent(TreeScope.Subtree),
                                new PropertyChangeEvent(TreeScope.Subtree, 
                                    AutomationElement.NameProperty,
                                    ValuePattern.ValueProperty),
                                new OrdinaryEvent(TextPattern.TextChangedEvent, TreeScope.Subtree)
                           };
            }
        }
    }
}
