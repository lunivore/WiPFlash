#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;

#endregion

namespace WiPFlash.Examples.Component
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

        [Test]
        public void ShouldBeAbleToWaitForSelection()
        {
            GivenThisWillHappenAtSomePoint(cb => cb.Selected = true);
            ThenWeShouldBeAbleToWaitFor(cb => cb.Selected.Equals(true));
        }

        protected override CheckBox CreateWrapperWith(AutomationElement element, string name)
        {
            return new CheckBox(element, name);
        }

        protected override CheckBox CreateWrapper()
        {
            return LaunchPetShopWindow().Find<CheckBox>("vatReceiptInput");
        }
    }
}