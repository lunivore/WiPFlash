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
    public class GridViewBehaviour : AutomationElementWrapperExamples<GridView>
    {
        private Window _window;

        [Test]
        public void ShouldProvideItsTextByCell()
        {
            var gridView = CreateWrapper();
            Assert.AreEqual("Dancer", gridView.TextAt(0, 2));
        }

        [Test]
        public void ShouldProvideAllAvailableText()
        {
            var gridView = CreateWrapper();
            Assert.AreEqual(3*4, gridView.AllText.Length);
            Assert.AreEqual("Dancer", gridView.AllText[2, 0]);
        }

        [Test]
        public void ShouldDetermineIfItContainsARow()
        {
            var gridView = CreateWrapper();
            Assert.IsTrue(gridView.ContainsRow("Dancer", "Rabbit", "54.00", "False"));
            Assert.IsFalse(gridView.ContainsRow("Prancer", "Reindeer", "30.00", "False"));
        }

        [Test]
        public void ShouldBeAbleToWaitForContentChanges()
        {
            GivenThisWillHappenAtSomePoint(view =>
                                               {
                                                   _window.Find<ComboBox>("basketPetInput").Select("Pet[Dancer]");
                                                   _window.Find<Button>("purchaseButton").Click();
                                               });
            ThenWeShouldBeAbleToWaitFor(view => view.TextAt(3, 2) == "True");
        }

        protected override GridView CreateWrapperWith(AutomationElement element, string name)
        {
            return new GridView(element, name);
        }

        protected override GridView CreateWrapper()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(new TitleBasedFinder(), "History").Select();
            return _window.Find<GridView>("lastPetsOutput");
        }
    }
}
