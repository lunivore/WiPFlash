using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Framework;

namespace WiPFlash.Examples.Framework
{
    [TestFixture]
    public class ConditionDescriberBehaviour
    {
        [Test]
        public void ShouldDescribeAPropertyConditionWithConditionAndValue()
        {
            var condition = new PropertyCondition(AutomationElement.NameProperty, "Wibble");
            const string expected = "AutomationElementIdentifiers.NameProperty='Wibble'";

            Assert.AreEqual(expected, new ConditionDescriber().Describe(condition));
        }

        [Test]
        public void ShouldDescribeCompoundConditionsUsingAnd()
        {
            var nameCondition = new PropertyCondition(AutomationElement.NameProperty, "Wobble");
            var controlTypeCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            var condition = new AndCondition(nameCondition, controlTypeCondition);

            var expected = "(AutomationElementIdentifiers.NameProperty='Wobble' and AutomationElementIdentifiers.ControlTypeProperty='" + ControlType.Window.Id + "')";

            Assert.AreEqual(expected, new ConditionDescriber().Describe(condition));
        }

        [Test]
        public void ShouldDescribeCompoundConditionsUsingOr()
        {
            var nameCondition = new PropertyCondition(AutomationElement.NameProperty, "Wubble");
            var controlTypeCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            var condition = new OrCondition(nameCondition, controlTypeCondition);

            var expected = "(AutomationElementIdentifiers.NameProperty='Wubble' or AutomationElementIdentifiers.ControlTypeProperty='" + ControlType.Window.Id + "')";

            Assert.AreEqual(expected, new ConditionDescriber().Describe(condition));
        }

        [Test]
        public void ShouldDescribeNegatedCompoundsUsingNot()
        {
            var nameCondition = new PropertyCondition(AutomationElement.NameProperty, "Wooble");
            const string expected = "not AutomationElementIdentifiers.NameProperty='Wooble'";

            Assert.AreEqual(expected, new ConditionDescriber().Describe(new NotCondition(nameCondition)));
        }

        [Test]
        public void ShouldDescribeTrueAndFalseAsTrueOrFalse()
        {
            Assert.AreEqual("true", new ConditionDescriber().Describe(Condition.TrueCondition));
            Assert.AreEqual("false", new ConditionDescriber().Describe(Condition.FalseCondition));
        }
    }
}
