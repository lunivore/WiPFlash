#region

using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Behavior.Component
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

        [Test]
        public void ShouldProvideCurrentItems()
        {
            var window = LaunchPetShopWindow();
            var listBox = window.Find<ListBox>("petRulesInput");
            var items = new List<string>(listBox.Items);
            Assert.True(items.Contains("Rule[Dangerous]"));
            Assert.True(items.Contains("Rule[No Children]"));
        }

        [Test]
        public void ShouldWaitForItemsToBeSelected()
        {
            var window = LaunchPetShopWindow();
            var listBox = window.Find<ListBox>("petRulesInput");

            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               listBox.Select("Rule[Dangerous]");
                           }).Start(null);

            Assert.True(listBox.WaitFor(
                            (src, e) => new List<string>(listBox.Selection).Contains("Rule[Dangerous]"),
                            src => Assert.Fail()));           
        }
    }
}