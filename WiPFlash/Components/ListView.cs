using System.Windows.Automation;

namespace WiPFlash.Components
{
    public class ListView : ListBox
    {
        public ListView(AutomationElement element, string name) : base(element, name)
        {
        }
    }
}
