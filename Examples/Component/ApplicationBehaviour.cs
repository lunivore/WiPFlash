#region

using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Component;
using WiPFlash.Components;
using WiPFlash.Exceptions;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class ApplicationBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldFindWindowByName()
        {
            Application application = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH);
            Window window = application.FindWindow("petShopWindow");
            Assert.IsNotNull(window.Element);
        }

        [Test]
        public void ShouldComplainIfWindowNotFound()
        {
            try
            {
                new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).FindWindow(
                    "wibbleWindow");
                Assert.Fail("Application should have complained when failing to find window");
            } catch (FailureToFindException) {}
        }
    }
}
