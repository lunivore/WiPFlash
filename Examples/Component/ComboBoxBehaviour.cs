using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Components;

namespace Examples.Component
{
    [TestFixture]
    public class ComboBoxBehaviour : AutomationElementWrapperExamples<ComboBox>
    {
        [Test]
        public void ShouldAllowValuesToBeSetWhenEditable()
        {
            ComboBox editableBox = CreateWrapper();
            editableBox.Select("");
            Assert.AreEqual("", editableBox.Text);
            editableBox.Select("Rabbit");
            Assert.AreEqual("Rabbit", editableBox.Text);
            editableBox.Select("WibbleBeast");
            Assert.AreEqual("WibbleBeast", editableBox.Text);
        }

        [Test]
        public void ShouldAllowValuesToBeSetWhenNotEditable()
        {
            Window window = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).FindWindow(EXAMPLE_APP_WINDOW_NAME);
            ComboBox nonEditableBox = window.Find<ComboBox>("petFoodInput");
            nonEditableBox.Select("");
            Assert.AreEqual("", nonEditableBox.Text);
            nonEditableBox.Select("Carnivorous");
            Assert.AreEqual("Carnivorous", nonEditableBox.Text);
        }

        protected override ComboBox CreateWrapperWith(AutomationElement element)
        {
            return new ComboBox(element);
        }

        protected override ComboBox CreateWrapper()
        {
            Window window = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).FindWindow(EXAMPLE_APP_WINDOW_NAME);
            return window.Find<ComboBox>("petTypeInput");
        }
    }
}
