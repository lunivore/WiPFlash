﻿#region

using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class ListBoxBehaviour : UIBasedExamples
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
    }
}
