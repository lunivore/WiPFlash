#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class TextBlock : AutomationElementWrapper
    {
        public TextBlock(AutomationElement element) : base(element)
        {
        }

        public string Text
        {
            get { return Element.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString(); }
        }
    }
}
