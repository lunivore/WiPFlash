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
    public class GridViewBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldProvideItsTextByCell()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            Assert.AreEqual("Dancer", gridView.TextAt(0, 2));
        }

        [Test]
        public void ShouldProvideAllAvailableText()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            Assert.AreEqual(3*4, gridView.AllText.Length);
            Assert.AreEqual("Dancer", gridView.AllText[2, 0]);
        }

        [Test]
        public void ShouldDetermineIfItContainsARow()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            Assert.IsTrue(gridView.ContainsRow("Dancer", "Rabbit", 54.00.ToString("0.00"), "False"));
            Assert.IsFalse(gridView.ContainsRow("Prancer", "Reindeer", 30.00.ToString("0.00"), "False"));
        }
        
        [Test]
        public void ShouldDetermineIfItContainsARowWithTheGivenHeaderAndPropertyCondition()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            Assert.True(gridView.ContainsRow("Name", FindBy.WpfText("Dancer")));
        }

        [Test]
        public void ShouldBeAbleToRetrieveTheRowIndexForARowWithTheGivenHeaderAndPropertyCondition()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            Assert.AreEqual(2, gridView.IndexOf("Name", FindBy.WpfText("Dancer")));
        }

        [Test]
        public void ShouldBeAbleToRetrieveTheElementReferencedByARowWithHeaderMatchingAPropertyConditionFromSecondHeader()
        {
            _window = LaunchPetShopWindow();
            _window.Find<Tab>(FindBy.WpfText("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            var cell = gridView.FindReferencedElement<Label>("Name", FindBy.WpfText("Dancer"), "Price");
            Assert.AreEqual(54.00.ToString("0.00"), cell.Text );
        }

        [Test]
        public void ShouldExposeItselfAsAListView()
        {
            var gridView = new GridView(AutomationElement.RootElement, "Fake grid");
            var listView = gridView.AsListView();
            Assert.AreSame(gridView.Element, listView.Element);
            Assert.AreEqual(typeof (ListView), listView.GetType());
        }

        [Test]
        public void ShouldTellUsIfTheGridViewIsEmpty()
        {
            // Given the petshop window
            _window = LaunchPetShopWindow();

            // Then the history should not be empty
            _window.Find<Tab>(FindBy.WpfTitle("History")).Select();
            var gridView = _window.Find<GridView>("lastPetsOutput");
            Assert.IsFalse(gridView.IsEmpty());

            // But the basket should
            _window.Find<Tab>(FindBy.WpfTitle("Basket")).Select();
            gridView = _window.Find<GridView>("basketOutput");
            Assert.IsTrue(gridView.IsEmpty());
        }
    }
}