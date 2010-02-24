#region

using System;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Exceptions;

#endregion

namespace Examples
{
    [TestFixture]
    public class ApplicationLauncherBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldStartUpApplicationOnRequest()
        {
            var launcher = new ApplicationLauncher();
            var app = launcher.Launch(EXAMPLE_APP_PATH);
            Console.WriteLine("Launched app at {0}, finding window", DateTime.Now);
            app.FindWindow(EXAMPLE_APP_WINDOW_NAME);
            Assert.IsNotNull(app.Process);
            Console.WriteLine("Example finished at {0}", DateTime.Now);
        }

        [Test]
        public void ShouldRecycleExistingApplicationIfRequested()
        {
            var launcher = new ApplicationLauncher();
            var originalApp = launcher.Launch(EXAMPLE_APP_PATH);
            originalApp.FindWindow(EXAMPLE_APP_WINDOW_NAME);

            var newApp = launcher.Recycle(EXAMPLE_APP_NAME);
            newApp.FindWindow(EXAMPLE_APP_WINDOW_NAME);

            Assert.AreEqual(originalApp.Process.Id, newApp.Process.Id);
        }

        [Test]
        public void ShouldStartUpAnApplicationOrRecycleAnExistingOneAsAppropriate()
        {
            var launcher = new ApplicationLauncher();
            var originalApp = launcher.LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH);
            Assert.IsNotNull(originalApp.Process);
            var newApp = launcher.LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH);
            Assert.AreEqual(originalApp.Process.Id, newApp.Process.Id);
        }

        [Test]
        public void ShouldComplainIfMoreThanOneProcessExistsToRecycle()
        {
            var launcher = new ApplicationLauncher();
            launcher.Launch(EXAMPLE_APP_PATH).FindWindow(EXAMPLE_APP_WINDOW_NAME);
            launcher.Launch(EXAMPLE_APP_PATH).FindWindow(EXAMPLE_APP_WINDOW_NAME);

            try
            {
                launcher.Recycle(EXAMPLE_APP_NAME);
                Assert.Fail("Launcher should have complained");
            } catch (FailureToLaunchException) {}

            try
            {
                launcher.LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH);
                Assert.Fail("Launcher should have complained");
            } catch (FailureToLaunchException) {}
        }

        [Test]
        public void ShouldComplainIfTheApplicationCantBeLaunched()
        {
            var launcher = new ApplicationLauncher();
            try
            {
                launcher.Launch("Wibble.exe");
                Assert.Fail("Launcher should have complained");
            } catch (WiPFlashException){}

            try
            {
                launcher.LaunchOrRecycle("Wibble", "Wibble.exe");
                Assert.Fail("Launcher should have complained");
            }
            catch (WiPFlashException) { }
        }


        [Test]
        public void ShouldComplainIfTheApplicationCantBeRecycled()
        {
            var launcher = new ApplicationLauncher();
            try
            {
                launcher.Recycle("Wibble.exe");
                Assert.Fail("Launcher should have complained");
            }
            catch (WiPFlashException) { }
        }
    }
}
