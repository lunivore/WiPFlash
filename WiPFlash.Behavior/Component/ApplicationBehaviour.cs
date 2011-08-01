#region

using System;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;
using WiPFlash.Exceptions;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Behavior.Component
{
    [TestFixture]
    public class ApplicationBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldFindWindowByName()
        {
            Application application = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, Assert.Fail);
            Window window = application.FindWindow("petShopWindow");
            Assert.IsNotNull(window.Element);
        }

        [Test]
        public void ShouldFindWindowByCondition()
        {
            Application application = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, Assert.Fail);
            Window window = application.FindWindow(new AndCondition(
                                                       FindBy.WpfName("petShopWindow"), FindBy.ControlType(ControlType.Window)));
            Assert.IsNotNull(window.Element);
        }

        [Test]
        public void ShouldComplainIfWindowNotFound()
        {
            try
            {
                new ApplicationLauncher(TimeSpan.Parse("00:00:01")).LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, Assert.Fail).FindWindow(
                    "wibbleWindow");
                Assert.Fail("Application should have complained when failing to find window");
            } catch (FailureToFindException) {}
        }

        [Test]
        public void ShouldWaitForTheSpecifiedTimeoutAndStopAsSoonAsTheWindowIsFound()
        {
            TimeSpan farFarTooLong = TimeSpan.Parse("00:01:00");
            TimeSpan longEnough = System.TimeSpan.Parse("00:00:10");

            
            var started = DateTime.Now;

            Application application = new ApplicationLauncher(farFarTooLong).LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, Assert.Fail);
            Window window = application.FindWindow(EXAMPLE_APP_WINDOW_NAME);


            var finished = DateTime.Now;
            var timeBetween = finished.Subtract(started);

            Assert.LessOrEqual(timeBetween, longEnough);
        }

        [Test]
        public void ShouldBeAbleToTellWhetherAWindowExistsOrNotWithinTheSpecifiedAmountOfTime()
        {
            TimeSpan farFarTooLong = TimeSpan.Parse("00:01:00");
            TimeSpan longEnough = TimeSpan.Parse("00:00:05");
            TimeSpan aBitExtraForProcessing = TimeSpan.Parse("00:00:01");

            var started = DateTime.Now;

            Application application = new ApplicationLauncher(farFarTooLong).LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, Assert.Fail);

            bool windowDoesNotExist = false;
            application.FindWindow("Wibble", longEnough, (message) => windowDoesNotExist = true);
            var finished = DateTime.Now;
            var timeBetween = finished.Subtract(started);

            Assert.IsTrue(windowDoesNotExist);
            Assert.LessOrEqual(timeBetween, longEnough.Add(aBitExtraForProcessing));

            windowDoesNotExist = false;
            started = DateTime.Now;
            application.FindWindow(new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Wibble"),
                                                    new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                                          ControlType.Window)),
                                   longEnough,
                                   message => windowDoesNotExist = true);
            finished = DateTime.Now;
            timeBetween = finished.Subtract(started);

            Assert.IsTrue(windowDoesNotExist);
            Assert.LessOrEqual(timeBetween, longEnough.Add(aBitExtraForProcessing));
        }
    }
}