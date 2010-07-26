using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
