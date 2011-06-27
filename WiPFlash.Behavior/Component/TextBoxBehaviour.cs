#region

using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Behavior.Component
{
    [TestFixture]
    public class TextBoxBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowTextToBeEnteredIntoTheTextBox()
        {
            var window = LaunchPetShopWindow();
            var box = window.Find<TextBox>("petNameInput");
            box.Text = "Gooseberry Bear";
            Assert.AreEqual("Gooseberry Bear", box.Text);
        }

        [Test]
        public void ShouldWaitForTextToBeChanged()
        {
            var window = LaunchPetShopWindow();
            var box = window.Find<TextBox>("petNameInput");
            new Thread((o) => {
                                  Thread.Sleep(100); 
                                  box.Text = "Fred"; 
            }).Start(null);

            Assert.IsTrue(box.WaitFor((src, e) => box.Text.Equals("Fred"), 
                                      src => Assert.Fail()));
        }
    }
}