#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
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
            foreach (var list in items)
            {
                Console.WriteLine(list);
            }
            Assert.True(items.Contains("Rule[Dangerous]"));
            Assert.True(items.Contains("Rule[No Children]"));

        }

        protected override ListBox CreateWrapperWith(AutomationElement element)
        {
            return new ListBox(element);
        }

        protected override ListBox CreateWrapper()
        {
            return LaunchPetShopWindow().Find<ListBox>("petRulesInput");
        }
    }
}
