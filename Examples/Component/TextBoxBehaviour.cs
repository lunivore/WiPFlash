#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class TextBoxBehaviour : AutomationElementWrapperExamples<TextBox>
    {
        [Test]
        public void ShouldAllowTextToBeEnteredIntoTheTextBox()
        {
            TextBox box = CreateWrapper();
            box.Text = "Gooseberry Bear";
            Assert.AreEqual("Gooseberry Bear", box.Text);
        }

        [Test]
        public void ShouldWaitForTextToBeChanged()
        {
            GivenThisWillHappenAtSomePoint(text => text.Text = "Hello!");
            ThenWeShouldBeAbleToWaitFor((text, e) => ((TextBox)text).Text.Equals("Hello!"));
        }

        protected override TextBox CreateWrapperWith(AutomationElement element, string name)
        {
            return new TextBox(element, name);
        }

        protected override TextBox CreateWrapper()
        {
            return FindPetShopElement("petNameInput");
        }
    }
}