#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class RichTextBoxBehaviour : AutomationElementWrapperExamples<RichTextBox>
    {
        [Test]
        public void ShouldAllowTextToBeEnteredIntoTheTextBox()
        {
            RichTextBox box = CreateWrapper();
            box.Text = "Gooseberry Bear";
            Assert.AreEqual("Gooseberry Bear", box.Text);
        }

        [Test]
        public void ShouldWaitForTheTextToChange()
        {
            GivenThisWillHappenAtSomePoint(box => box.Text = "Gooseberries");
            ThenWeShouldBeAbleToWaitFor((box, e) => ((RichTextBox)box).Text.Equals("Gooseberries"));
        }



        protected override RichTextBox CreateWrapperWith(AutomationElement element, string name)
        {
            return new RichTextBox(element, name);
        }

        protected override RichTextBox CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>(FindBy.WpfText("History")).Select();
            return window.Find<RichTextBox>("historyInput");
        }
    }
}