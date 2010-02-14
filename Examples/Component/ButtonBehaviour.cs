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
    public class ButtonBehaviour : AutomationElementWrapperExamples<Button>
    {
        [Test]
        public void ShouldBeClickable()
        {
            Button button = CreateWrapper();
            button.Click();
            // TODO Add something that makes clicking the button valuable - history of last changes would be good
        }

        protected override Button CreateWrapperWith(AutomationElement element)
        {
            return new Button(element);
        }

        protected override Button CreateWrapper()
        {
            Window window = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).FindWindow(
                EXAMPLE_APP_WINDOW_NAME);
            return window.Find<Button>("petSaveButton");
        }
    }
}
