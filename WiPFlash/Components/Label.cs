#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class Label : AutomationElementWrapper
    {
        public Label(AutomationElement element) : base(element)
        {
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }
    }
}
