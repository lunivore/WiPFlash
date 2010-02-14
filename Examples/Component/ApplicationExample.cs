using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Component;
using WiPFlash.Exceptions;

namespace Examples.Component
{
    [TestFixture]
    public class ApplicationExample : UIBasedExample
    {
        [Test]
        public void ShouldFindWindowByName()
        {
            Application application = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH);
            Window window = application.GetWindow("petShopWindow");
            Assert.IsNotNull(window.AutomationElement);
        }

        [Test]
        public void ShouldComplainIfWindowNotFound()
        {
            try
            {
                new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).GetWindow(
                    "wibbleWindow");
                Assert.Fail("Application should have complained when failing to find window");
            } catch (FailureToFindException) {}
        }
    }
}
