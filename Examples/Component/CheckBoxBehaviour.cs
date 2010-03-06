#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class CheckBoxBehaviour : AutomationElementWrapperExamples<CheckBox>
    {
        [Test]
        public void ShouldAllowItselfToBeToggled()
        {
            CheckBox checkBox = CreateWrapper();
            Assert.False(checkBox.Selected);
            checkBox.Toggle();
            Assert.True(checkBox.Selected);
            checkBox.Toggle();
            Assert.False(checkBox.Selected);
        }

        [Test]
        public void ShouldAllowItselfToBeSelected()
        {
            CheckBox checkBox = CreateWrapper();
            checkBox.Selected = true;
            Assert.True(checkBox.Selected);
            checkBox.Selected = false;
            Assert.False(checkBox.Selected);
        }

        protected override CheckBox CreateWrapperWith(AutomationElement element)
        {
            return new CheckBox(element);
        }

        protected override CheckBox CreateWrapper()
        {
            return LaunchPetShopWindow().Find<CheckBox>("vatReceiptInput");
        }
    }
}
