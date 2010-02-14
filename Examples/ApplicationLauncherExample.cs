#region

using System.Diagnostics;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Exceptions;

#endregion

namespace Examples
{
    [TestFixture]
    public class ApplicationLauncherExample
    {
        private const string EXAMPLE_APP_NAME = "ExampleUIs";

        private const string EXAMPLE_APP_PATH =
            @"..\..\..\" + EXAMPLE_APP_NAME + @"\bin\debug\" + EXAMPLE_APP_NAME + ".exe";

        [Test]
        public void ShouldStartUpApplicationOnRequest()
        {
            var launcher = new ApplicationLauncher();
            var app = launcher.Launch(EXAMPLE_APP_PATH);
            Assert.IsNotNull(app.Process);
        }

        [Test]
        public void ShouldRecycleExistingApplicationIfRequested()
        {
            var launcher = new ApplicationLauncher();
            var originalApp = launcher.Launch(EXAMPLE_APP_PATH);
            var newApp = launcher.Recycle(EXAMPLE_APP_NAME);
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
            launcher.Launch(EXAMPLE_APP_PATH);
            launcher.Launch(EXAMPLE_APP_PATH);

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

        [TearDown]
        public void CloseAllExampleApplications()
        {
            Process[] processes = Process.GetProcessesByName(EXAMPLE_APP_NAME);
            foreach (var process in processes)
            {
                process.CloseMainWindow();
            }
        }
    }
}
