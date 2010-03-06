#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class RadioButtonBehaviour : AutomationElementWrapperExamples<RadioButton>
    {
        [Test]
        public void ShouldAllowItselfToBeSelected()
        {
            RadioButton radioButton = CreateWrapper();
            Assert.False(radioButton.Selected);
            radioButton.Select();
            Assert.True(radioButton.Selected);
        }

        protected override RadioButton CreateWrapperWith(AutomationElement element)
        {
            return new RadioButton(element);
        }

        protected override RadioButton CreateWrapper()
        {
            return LaunchPetShopWindow().Find<RadioButton>("cardPaymentInput");
        }
    }
}
