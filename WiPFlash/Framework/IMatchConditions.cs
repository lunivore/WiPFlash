using System.Windows.Automation;

namespace WiPFlash.Framework
{
    public interface IMatchConditions
    {
        bool Matches(AutomationElement element, Condition condition);
    }
}