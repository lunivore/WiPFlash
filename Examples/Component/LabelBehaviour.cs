#region

using System.Threading;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class LabelBehaviour : AutomationElementWrapperExamples<Label>
    {
        [Test]
        public void ShouldAllowTextToBeRetrievedFromBlock()
        {
            Label label = CreateWrapper();
            Assert.AreEqual("0.00", label.Text);
        }

        [Test]
        public void ShouldWaitForTheLabelTextToChange()
        {
            Window window = LaunchPetShopWindow();
            Label label = window.Find<Label>("totalOutput");
            new Thread(() =>
                           {
                               Thread.Sleep(200);
                               window.Find<ComboBox>("basketInput").Select("Pet[Cinnamon]");
                           }).Start();
            label.WaitFor(l => l.Text.Equals("4.50"));
            Assert.AreEqual("4.50", label.Text);
        }

        protected override Label CreateWrapperWith(AutomationElement element, string name)
        {
            return new Label(element, name);
        }

        protected override Label CreateWrapper()
        {
            return LaunchPetShopWindow().Find<Label>("totalOutput");
        }
    }
}
