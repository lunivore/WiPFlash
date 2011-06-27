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
    public class LabelBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowTextToBeRetrievedFromBlock()
        {
            var label = LaunchPetShopWindow().Find<Label>("totalOutput");
            Assert.AreEqual("0.00", label.Text);
        }

        [Test]
        public void ShouldWaitForTheLabelTextToChange()
        {
            var window = LaunchPetShopWindow();
            var label = window.Find<Label>("totalOutput");
            new Thread(() =>
                           {
                               Thread.Sleep(200);
                               window.Find<ComboBox>("basketPetInput").Select("Pet[Cinnamon]");
                           }).Start();
            label.WaitFor((l, e) => ((Label)l).Text.Equals("4.50"), e => Assert.Fail("Should have waited for label to be 4.50"));
            Assert.AreEqual("4.50", label.Text);
        }
    }
}