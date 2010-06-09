#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class ScrollViewerBehaviour : AutomationElementWrapperExamples<ScrollViewer>
    {
        [Test]
        public void ShouldScrollUntilCheckIsTrue()
        {
            var scrollViewer = CreateWrapper();
            scrollViewer.ScrollDown(s => s.Contains(new PropertyBasedFinder(AutomationElement.NameProperty), "Flea powder"), s => Assert.Fail("Should have found row"));
        }

        [Test]
        public void ShouldHandleFailureToScroll()
        {

            var scrollViewer = CreateWrapper();
            var complained = false;
            scrollViewer.ScrollDown(s => false, (s) => complained = true);
            Assert.True(complained, "Should have handled failure to scroll");
        }

        protected override ScrollViewer CreateWrapperWith(AutomationElement element, string name)
        {
            return new ScrollViewer(element, name);
        }

        protected override ScrollViewer CreateWrapper()
        {
            var tab = LaunchPetShopWindow().Find<Tab>(new TitleBasedFinder(), "Accessories");
            tab.Select();
            return tab.Find<ScrollViewer>(new PropertyBasedFinder(AutomationElement.IsScrollPatternAvailableProperty), true);
        }
    }
}
