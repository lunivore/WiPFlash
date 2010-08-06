#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class TabBehaviour : AutomationElementWrapperExamples<Tab>
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
        public void ShouldWaitForTheTabToGetFocus()
        {
            GivenThisWillHappenAtSomePoint(tab => tab.Select());
            ThenWeShouldBeAbleToWaitFor((tab, e) => ((Tab)tab).HasFocus());
        }

        protected override Tab CreateWrapperWith(AutomationElement element, string name)
        {
            return new Tab(element, name);
        }

        protected override Tab CreateWrapper()
        {
            return LaunchPetShopWindow().Find<Tab>(FindBy.WpfText("History"));
        }
    }
}