#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class Button : AutomationElementWrapper
    {
        public Button(AutomationElement element) : base(element)
        {
        }

        public void Click()
        {
            ((InvokePattern)Element.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        }
    }
}