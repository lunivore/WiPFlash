#region

using System.Collections.Generic;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class ListBoxBehaviour : AutomationElementWrapperExamples<ListBox>
    {
        [Test]
        public void ShouldAllowItemsToBeSelected()
        {
            var window = LaunchPetShopWindow();
            var listBox = window.Find<ListBox>("petRulesInput");
            listBox.Select("Rule[Dangerous]");
            listBox.Select("Rule[No Children]");

            Assert.AreEqual("Rule[Dangerous]", listBox.Selection[0]);
            Assert.AreEqual("Rule[No Children]", listBox.Selection[1]);
        }

        [Test]
        public void ShouldAllowSelectionToBeCleared()
        {
            var window = LaunchPetShopWindow();
            var listBox = window.Find<ListBox>("petRulesInput");

            listBox.Select("Rule[Dangerous]");
            listBox.ClearSelection();
            Assert.AreEqual(0, listBox.Selection.Length);
        }

        [Test]
        public void ShouldProvideCurrentItems()
        {
            ListBox listBox = CreateWrapper();
            var items = new List<string>(listBox.Items);
            Assert.True(items.Contains("Rule[Dangerous]"));
            Assert.True(items.Contains("Rule[No Children]"));
        }

        [Test]
        public void ShouldBeAbleToWaitForSelectionAndContentChanges()
        {
            GivenThisWillHappenAtSomePoint(list => list.Select("Rule[Dangerous]"));
            ThenWeShouldBeAbleToWaitFor((list, e) => new List<string>(((ListBox)list).Selection).Contains("Rule[Dangerous]"));
        }

        protected override ListBox CreateWrapperWith(AutomationElement element, string name)
        {
            return new ListBox(element, name);
        }

        protected override ListBox CreateWrapper()
        {
            return LaunchPetShopWindow().Find<ListBox>("petRulesInput");
        }
    }
}