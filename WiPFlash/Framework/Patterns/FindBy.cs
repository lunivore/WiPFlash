using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Framework.Patterns
{
    public static class FindBy
    {
        public static PropertyCondition AutomationId(string id)
        {
            return new PropertyCondition(AutomationElement.AutomationIdProperty, id);
        }
    }
}
