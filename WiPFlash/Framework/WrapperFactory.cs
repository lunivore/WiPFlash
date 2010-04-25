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
            if (creatorsByType.ContainsKey(typeof (T)))
            {
                return (T) creatorsByType[typeof (T)](element, name);
            }
            var constructor = typeof (T).GetConstructor(new[] {typeof (AutomationElement), typeof (string)});
         
            if (constructor != null)
            {
                return (T) constructor.Invoke(new object[] {element, name});
            }
            
            constructor = typeof (T).GetConstructor(new[] {typeof (AutomationElement)});
            
            if (constructor != null)
            {

                return (T) constructor.Invoke(new object[] {element});
            }
            throw new ArgumentException("No suitable constructor found for WiPFlash component of type {0}. " +
                                        "Should be either (AutomationElement, string) or just (AutomationElement)");
        }
    }
}