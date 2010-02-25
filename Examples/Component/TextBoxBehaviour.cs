#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Components;

#endregion

namespace Examples.Component
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

        protected override TextBox CreateWrapperWith(AutomationElement element)
        {
            return new TextBox(element);
        }

        protected override TextBox CreateWrapper()
        {
            return FindPetShopElement("petNameInput");
        }
    }
}
