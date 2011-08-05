using System.Windows.Automation;

namespace WiPFlash.Framework
{
    public class ConditionMatcher : IMatchConditions
    {
        public bool Matches(AutomationElement element, Condition condition)
        {
            return new TreeWalker(condition).Normalize(element) != null;
        }
    }
}
