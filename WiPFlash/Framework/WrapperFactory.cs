#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Framework
{
    public class WrapperFactory : IWrapAutomationElements
    {
        private delegate AutomationElementWrapper CreatesWrapperForType(AutomationElement element);

        private static readonly IDictionary<Type, CreatesWrapperForType> creatorsByType 
            = new Dictionary<Type, CreatesWrapperForType>
            {
                {typeof(RichTextBox), (element) => new RichTextBox(element)},
                {typeof(TextBox), (element) => new TextBox(element)},
                {typeof(TextBlock), (element) => new TextBlock(element)},
                {typeof(ListBox), (element) => new ListBox(element)},
                {typeof(Button), (element) => new Button(element)},
                {typeof(Tab), (element) => new Tab(element)},
                {typeof(EditableComboBox), (element) => new EditableComboBox(element)},
                {typeof(ComboBox), (element) =>
                                       {
                                           var patterns = new List<AutomationPattern>(element.GetSupportedPatterns());
                                           return patterns.Contains(ValuePattern.Pattern) ?
                                               new EditableComboBox(element) :
                                               new ComboBox(element);
                                       }
                }
            };

        public T Wrap<T>(AutomationElement element) where T : AutomationElementWrapper
        {
            return (T)creatorsByType[typeof (T)](element);
        }
    }
}