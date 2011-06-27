#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Behavior.Component
{
    [TestFixture]
    public class ScrollViewerBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldScrollUntilCheckIsTrue()
        {
            var tab = LaunchPetShopWindow().Find<Tab>(FindBy.WpfText("Accessories"));
            tab.Select();
            var scrollViewer = tab.Find<ScrollViewer>(new PropertyCondition(AutomationElement.IsScrollPatternAvailableProperty, true));
            scrollViewer.ScrollDown(s => s.Contains(FindBy.WpfText("Flea powder")), s => Assert.Fail("Should have found row"));
        }

        [Test]
        public void ShouldHandleFailureToScroll()
        {
            var tab = LaunchPetShopWindow().Find<Tab>(FindBy.WpfText("Accessories"));
            tab.Select();
            var scrollViewer = tab.Find<ScrollViewer>(new PropertyCondition(AutomationElement.IsScrollPatternAvailableProperty, true));
            var complained = false;
            scrollViewer.ScrollDown(s => false, s => complained = true);
            Assert.True(complained, "Should have handled failure to scroll");
        }
    }
}