using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

namespace Examples.Component
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


        protected override RichTextBox CreateWrapperWith(AutomationElement element)
        {
            return new RichTextBox(element);
        }

        protected override RichTextBox CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>("historyTab").Select();
            return window.Find<RichTextBox>("historyInput");
        }
    }
}
