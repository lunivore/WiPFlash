using System;
using System.Windows.Automation;

namespace WiPFlash.Framework
{
    public static class FindBy
    {
        public static PropertyCondition WpfName(string name)
        {
            return UiAutomationId(name);
        }

        public static PropertyCondition UiAutomationId(string id)
        {
            return new PropertyCondition(AutomationElement.AutomationIdProperty, id);
        }

        public static PropertyCondition UiAutomationName(string name)
        {
            return new PropertyCondition(AutomationElement.NameProperty, name);
        }

        public static PropertyCondition WpfTitle(string text)
        {
            return UiAutomationName(text);
        }

        public static PropertyCondition WpfText(string text)
        {
            return UiAutomationName(text);
        }

        public static PropertyCondition Class(Type type)
        {
            return new PropertyCondition(AutomationElement.ClassNameProperty, type.Name);
        }

        public static PropertyCondition ControlType(ControlType type)
        {
            return new PropertyCondition(AutomationElement.ControlTypeProperty, type);
        }
    }
}
