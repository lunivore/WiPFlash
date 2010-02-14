using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Components;

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
            Window window = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).FindWindow(
                EXAMPLE_APP_WINDOW_NAME);
            return window.Find<TextBox>("petNameInput");
        }
    }
}
