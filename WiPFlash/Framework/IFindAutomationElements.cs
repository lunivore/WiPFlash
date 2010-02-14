using System.Windows.Automation;
using WiPFlash.Components;

namespace WiPFlash.Framework
{
    public interface IFindAutomationElements
    {
        T Find<T>(AutomationElementWrapper wrapper, string automationId) where T : AutomationElementWrapper;        
    }
}