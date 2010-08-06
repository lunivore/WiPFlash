#region

using System;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class TextBlockBehaviour : AutomationElementWrapperExamples<TextBlock>
    {
        [Test]
        public void ShouldAllowTextToBeRetrievedFromBlock()
        {
            TextBlock block = CreateWrapper();
            Assert.AreEqual("History so far:" + Environment.NewLine, block.Text);
        }

        [Test]
        public void ShouldWaitForContentsOfBlockToChange()
        {
            
            var window = LaunchPetShopWindow();
            var tab = window.Find<Tab>(FindBy.WpfText("History"));
            tab.Select();
            new Thread(() =>
                           {                               
                               var box = window.Find<RichTextBox>("historyInput");
                               box.Text = string.Empty;
                           }).Start();

            var block = window.Find<TextBlock>("historyOutput");
            block.WaitFor((b, e) => ((TextBlock)b).Text.Equals(string.Empty), (b) => Assert.Fail("Should have had empty text"));
            Assert.AreEqual(string.Empty, block.Text);
        }

        protected override TextBlock CreateWrapperWith(AutomationElement element, string name)
        {
            return new TextBlock(element, name);
        }

        protected override TextBlock CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>(FindBy.WpfText("History")).Select();
            return window.Find<TextBlock>("historyOutput");
        }
    }
}