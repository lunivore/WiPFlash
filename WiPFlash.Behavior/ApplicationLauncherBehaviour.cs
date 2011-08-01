#region

using System;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Behavior
{
    [TestFixture]
    public class ApplicationLauncherBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldStartUpApplicationOnRequest()
        {
            var launcher = new ApplicationLauncher();
            var app = launcher.Launch(EXAMPLE_APP_PATH, s => Assert.Fail("Should have launched the app"));
            app.FindWindow(EXAMPLE_APP_WINDOW_NAME);
            Assert.IsNotNull(app.Process);
        }

        [Test]
        public void ShouldRecycleExistingApplicationIfRequested()
        {
            var launcher = new ApplicationLauncher();
            var originalApp = launcher.Launch(EXAMPLE_APP_PATH, s => Assert.Fail("Should have launched the app"));
            originalApp.FindWindow(EXAMPLE_APP_WINDOW_NAME);

            var newApp = launcher.Recycle(EXAMPLE_APP_NAME, s => Assert.Fail("Should have launched the app"));
            newApp.FindWindow(EXAMPLE_APP_WINDOW_NAME);

            Assert.AreEqual(originalApp.Process.Id, newApp.Process.Id);
        }

        [Test]
        public void ShouldStartUpAnApplicationOrRecycleAnExistingOneAsAppropriate()
        {
            var launcher = new ApplicationLauncher();
            var originalApp = launcher.LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, s => Assert.Fail("Should have launched the app"));
            originalApp.FindWindow(EXAMPLE_APP_WINDOW_NAME);
            var newApp = launcher.LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, s => Assert.Fail("Should have launched the app"));
            newApp.FindWindow(EXAMPLE_APP_WINDOW_NAME);
            Assert.AreEqual(originalApp.Process.Id, newApp.Process.Id);
        }

        [Test]
        public void ShouldComplainIfMoreThanOneProcessExistsToRecycle()
        {
            var launcher = new ApplicationLauncher();
            launcher.Launch(EXAMPLE_APP_PATH, s => Assert.Fail("Should have launched the app"))
                .FindWindow(EXAMPLE_APP_WINDOW_NAME);
            launcher.Launch(EXAMPLE_APP_PATH, s => Assert.Fail("Should have launched the app"))
                .FindWindow(EXAMPLE_APP_WINDOW_NAME);

            var complained = false;
            launcher.Recycle(EXAMPLE_APP_NAME, s => complained = true);
            Assert.True(complained, "Launcher should have complained because two apps with the same name exist");

            complained = false;
            launcher.LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, s => complained = true);
            Assert.True(complained, "Launcher should have complained because two apps with the same name exist");
        }

        [Test]
        public void ShouldComplainIfTheApplicationCantBeLaunched()
        {
            var launcher = new ApplicationLauncher();
            var complained = false;
            launcher.Launch("Wibble.exe", s => complained = true);
            Assert.True(complained, "Launcher should have complained because the app could not be launched");
            
            complained = false;
            launcher.LaunchOrRecycle("Wibble", "Wibble.exe", s => complained = true);
            Assert.True(complained, "Launcher should have complained because the app could not be launched");
        }

        [Test]
        public void ShouldComplainIfTheApplicationCantBeRecycled()
        {
            var launcher = new ApplicationLauncher();
            var complained = false;
            launcher.Recycle("Wibble.exe", s => complained = true);
            Assert.True(complained, "Launcher should have complained because the app could not be recycled");
        }

        [Test]
        public void ShouldAllowUsToLaunchViaALauncher()
        {
            Application application =
                new ApplicationLauncher(TimeSpan.Parse("00:00:10")).LaunchVia(
                    EXAMPLE_APP_NAME, "LauncherToOpenAWindowBeforeTheOneWeWant.bat", Assert.Fail);
            application.FindWindow(EXAMPLE_APP_WINDOW_NAME);
        }
    }
}