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
    public class GridViewBehaviour : AutomationElementWrapperExamples<GridView>
    {
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
        public void ShouldDetermineIfItContainsARowWithTheGivenHeaderAndPropertyCondition()
        {
            var gridView = CreateWrapper();
            Assert.True(gridView.ContainsRow("Name", FindBy.WpfText("Dancer")));
        }

        [Test]
        public void ShouldBeAbleToRetrieveTheRowIndexForARowWithTheGivenHeaderAndPropertyCondition()
        {
            var gridView = CreateWrapper();
            Assert.AreEqual(2, gridView.IndexOf("Name", FindBy.WpfText("Dancer")));
        }

        [Test]
        public void ShouldBeAbleToRetrieveTheElementReferencedByARowWithHeaderMatchingAPropertyConditionFromSecondHeader()
        {
            var gridView = CreateWrapper();
            var cell = gridView.FindReferencedElement<Label>("Name", FindBy.WpfText("Dancer"), "Price");
            Assert.AreEqual("54.00", cell.Text );
        }

        [Test]
        public void ShouldBeAbleToWaitForContentChanges()
        {
            GivenThisWillHappenAtSomePoint(view =>
                                               {
                                                   _window.Find<ComboBox>("basketPetInput").Select("Pet[Dancer]");
                                                   _window.Find<Button>("purchaseButton").Click();
                                               });
            ThenWeShouldBeAbleToWaitFor((view, e) => ((GridView)view).TextAt(3, 2) == "True");
        }

        [Test]
        public void ShouldExposeItselfAsAListView()
        {
            var gridView = new GridView(AutomationElement.RootElement, "Fake grid");
            var listView = gridView.AsListView();
            Assert.AreSame(gridView.Element, listView.Element);
            Assert.AreEqual(typeof (ListView), listView.GetType());
        }

        protected override GridView CreateWrapperWith(AutomationElement element, string name)
        {
            return new GridView(element, name);
        }

        protected override GridView CreateWrapper()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            return _window.Find<GridView>("lastPetsOutput");
        }
    }
}