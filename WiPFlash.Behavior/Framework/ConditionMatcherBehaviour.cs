using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Framework;

namespace WiPFlash.Behavior.Framework
{
    [TestFixture]
    public class ConditionMatcherBehaviour
    {
        [Test]
        public void ShouldMatchACondition()
        {
            var conditionMatcher = new ConditionMatcher();
            var matchingCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane);
            Assert.True(conditionMatcher.Matches(AutomationElement.RootElement, matchingCondition));

            var nonMatchingCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "Wibble");
            Assert.False(conditionMatcher.Matches(AutomationElement.RootElement, nonMatchingCondition));
        }
    }
}