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
        private delegate object CreatesWrapperForType(AutomationElement element, string name);

        private static readonly IDictionary<Type, CreatesWrapperForType> creatorsByType 
            = new Dictionary<Type, CreatesWrapperForType>
            {
                {typeof(RichTextBox), (element, name) => new RichTextBox(element, name)},
                {typeof(TextBox), (element, name) => new TextBox(element, name)},
                {typeof(TextBlock), (element, name) => new TextBlock(element, name)},
                {typeof(ListBox), (element, name) => new ListBox(element, name)},
                {typeof(Button), (element, name) => new Button(element, name)},
                {typeof(CheckBox), (element, name) => new CheckBox(element, name)},
                {typeof(RadioButton), (element, name) => new RadioButton(element, name)},
                {typeof(Tab), (element, name) => new Tab(element, name)},
                {typeof(EditableComboBox), (element, name) => new EditableComboBox(element, name)},
                {typeof(Label), (element, name) => new Label(element, name)},
                {typeof(ComboBox), (element, name) =>
                                       {
                                           var patterns = new List<AutomationPattern>(element.GetSupportedPatterns());
                                           return patterns.Contains(ValuePattern.Pattern) ?
                                               new EditableComboBox(element, name) :
                                               new ComboBox(element, name);
                                       }
                }
            };

        public T Wrap<T>(AutomationElement element, string name) where T : AutomationElementWrapper<T>
        {
            return (T)creatorsByType[typeof (T)](element, name);
        }
    }
}