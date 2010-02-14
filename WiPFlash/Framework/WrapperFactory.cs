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
                {typeof(TextBox), (element) => new TextBox(element)},
                {typeof(ComboBox), (element) => new ComboBox(element)},
                {typeof(ListBox), (element) => new ListBox(element)},
                {typeof(Button), (element) => new Button(element)}
            };

        public T Wrap<T>(AutomationElement element) where T : AutomationElementWrapper
        {
            return (T)creatorsByType[typeof (T)](element);
        }
    }
}