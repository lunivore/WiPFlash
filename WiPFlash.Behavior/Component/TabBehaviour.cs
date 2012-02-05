#region

using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Behavior.Component
{
    [TestFixture]
    public class TabBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowTabToBeSelected()
        {
            var tab = LaunchPetShopWindow().Find<Tab>(FindBy.WpfText("History"));

            Assert.False(tab.HasFocus());

            tab.Select();
            Assert.True(tab.HasFocus());
        }

        [Test]
        public void ShouldProvideItsTitle()
        {
            var tab = LaunchPetShopWindow().Find<Tab>(FindBy.WpfText("History"));
            Assert.AreEqual("History", tab.Title);
        }

        [Test]
        public void ShouldWaitForTabToBeSelected()
        {

            var tab = LaunchPetShopWindow().Find<Tab>(FindBy.WpfText("History"));

            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               tab.Select();
                           }).Start(null);

            Assert.True(tab.WaitFor(
                            (src, e) => tab.HasFocus(),
                            src => Assert.Fail()));
        }
    }
}