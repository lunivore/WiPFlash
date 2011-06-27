#region

using System;
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
    public class TextBlockBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowTextToBeRetrievedFromBlock()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>(FindBy.WpfText("History")).Select();
            var block = window.Find<TextBlock>("historyOutput");
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
            block.WaitFor((b, e) => ((TextBlock)b).Text.Equals(string.Empty), b => Assert.Fail("Should have had empty text"));
            Assert.AreEqual(string.Empty, block.Text);
        }
    }
}