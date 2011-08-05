using System.Windows.Automation;

namespace WiPFlash.Framework
{
    public interface IDescribeConditions
    {
        string Describe(Condition condition);
    }
}