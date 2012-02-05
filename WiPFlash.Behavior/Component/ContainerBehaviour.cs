#region

using System.Linq;
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