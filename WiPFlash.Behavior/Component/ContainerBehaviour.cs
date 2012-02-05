#region

using System.Linq;
using System.Threading;
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
    public class ContainerBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldHandleFailureToFindAComponentByThrowingAnExceptionByDefault()
        {
            var container = new MyContainer();
            try
            {
                container.Find<ComboBox>("Wibble");
                Assert.Fail("Should have thrown an exception");
            }
            catch(FailureToFindException) { }
        }

        [Test]
        public void ShouldAllowTheFailureToFindHandlerToBeSet()
        {
            var container = new MyContainer();
            var complained = true;
            container.HandlerForFailingToFind = (s) => complained = true;
            container.Find<ComboBox>("Wibble");
            
            Assert.True(complained, "Should have handled failure to find using the given handler");
        }

        [Test]
        public void ShouldFindAllMatchingItemsIfRequested()
        {
            var window = LaunchPetShopWindow();
            var tabs = window.FindAll<Tab>(FindBy.ControlType(ControlType.TabItem));
            var tabTitles = tabs.Select(t => t.Title).ToList();

            Assert.Contains("Registration", tabTitles);
            Assert.Contains("History", tabTitles);
            Assert.Contains("Basket", tabTitles);
            Assert.Contains("Accessories", tabTitles);
        }

        [Test]
        public void ShouldBeAbleToWaitForAChildElementToAppear()
        {
            var window = LaunchPetShopWindow();

            new Thread(() =>
                           {
                               Thread.Sleep(100);
                               window.Find<Tab>(FindBy.WpfTitle("Accessories")).Select();
                           }).Start();

            var scrollViewer = window.WaitForElement<ScrollViewer>(new AndCondition(
                new PropertyCondition(AutomationElement.IsScrollPatternAvailableProperty, true),
                new PropertyCondition(ScrollPatternIdentifiers.VerticallyScrollableProperty, true)),
                e => Assert.Fail("Could not find the scroll viewer"));
            bool scrolledABit = false;
            scrollViewer.ScrollDown(s => scrolledABit, s => { scrolledABit = true; });

        }
    }

    public class MyContainer : Container
    {
        public MyContainer()
            : base(AutomationElement.RootElement)
        {
        }

        public MyContainer(AutomationElement e, string name)
            : base(e, name)
        {
        }
    }
}