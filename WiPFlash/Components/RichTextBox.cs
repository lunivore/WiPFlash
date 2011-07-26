#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;
using WiPFlash.Exceptions;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class RichTextBox : AutomationElementWrapper
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
                EmptyTextFromChild(Element);
                AutomationElement element = FindFirstChildWithValuePatternSupported(Element);
                if (element == null) 
                { 
                    throw new FailureToSetTextException("Could not set the text on " + Name + "; " +
                        "WiPFlash only supports a RichTextBox with an element supporting the Value Pattern, " +
                        "such as a TextBox.");
                }
                ((ValuePattern)element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(value);
            }
        }

        private AutomationElement FindFirstChildWithValuePatternSupported(AutomationElement element)
        {
            if (element.GetSupportedPatterns().Contains(ValuePattern.Pattern))
            {
                return element;
            }
            if (element.GetSupportedPatterns().Contains(TextPattern.Pattern))
            {
                TextPatternRange document =
                    ((TextPattern)Element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange;
                AutomationElement[] children = document.GetChildren();
                foreach (var child in children)
                {
                    var firstValidChild = FindFirstChildWithValuePatternSupported(child);
                    if (firstValidChild != null) { return firstValidChild; }
                }
            }
            return null;
        }

        private void EmptyTextFromChild(AutomationElement element)
        {
            if (element.GetSupportedPatterns().Contains(ValuePattern.Pattern))
            {
                ((ValuePattern)element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(string.Empty);
            }
            else if (element.GetSupportedPatterns().Contains(TextPattern.Pattern))
            {
                TextPatternRange document =
                    ((TextPattern)Element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange;
                AutomationElement[] children = document.GetChildren();
                foreach (var child in children)
                {
                    EmptyTextFromChild(child);
                }
            }
        }

        private string RetrieveTextFrom(AutomationElement element)
        {
            string text = string.Empty;
            if (element.GetSupportedPatterns().Contains(ValuePattern.Pattern))
            {
                text = text + new ValuePatternWrapper(element).Value;
            }
            else if (element.GetSupportedPatterns().Contains(TextPattern.Pattern))
            {
                TextPatternRange document =
                    ((TextPattern)Element.GetCurrentPattern(TextPattern.Pattern)).DocumentRange;
                AutomationElement[] children = document.GetChildren();
                foreach (var child in children)
                {
                    text = text + RetrieveTextFrom(child);
                }
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
