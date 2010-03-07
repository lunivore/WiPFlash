#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class ButtonBehaviour : AutomationElementWrapperExamples<Button>
    {
        [Test]
        public void ShouldBeClickable()
        {
            Button button = CreateWrapper();
            button.Click();
        }

        [Test]
        public void ShouldProvideItsText()
        {
            Button button = CreateWrapper();
            Assert.AreEqual("Save", button.Text);
        }

        protected override Button CreateWrapperWith(AutomationElement element, string name)
        {
            return new Button(element, name);
        }

        protected override Button CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            return window.Find<Button>("petSaveButton");
        }
    }
}
