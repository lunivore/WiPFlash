#region

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
    public class RichTextBoxBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowTextToBeEnteredIntoTheTextBox()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>(FindBy.WpfText("History")).Select();
            var box = window.Find<RichTextBox>("historyInput");
            box.Text = "Gooseberry Bear";
            Assert.AreEqual("Gooseberry Bear", box.Text);
        }

        [Test]
        public void ShouldWaitForTextToBeChanged()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>(FindBy.WpfText("History")).Select();
            var box = window.Find<RichTextBox>("historyInput");

            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               box.Text = "Fred";
                           }).Start(null);
            Assert.True(box.WaitFor(
                            (src, e) => box.Text.Equals("Fred"),
                            src => Assert.Fail()));
            
        }
    }
}