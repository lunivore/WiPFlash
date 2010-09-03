#region

using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class RadioButtonBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowItselfToBeSelected()
        {
            var radioButton = LaunchPetShopWindow().Find<RadioButton>("cardPaymentInput");
            Assert.False(radioButton.Selected);
            radioButton.Select();
            Assert.True(radioButton.Selected);
        }

        [Test]
        public void ShouldWaitForSelection()
        {
            var radioButton = LaunchPetShopWindow().Find<RadioButton>("cardPaymentInput");
            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               radioButton.Select();;
                           }).Start(null);
            Assert.True(radioButton.WaitFor(
                (src, e) => radioButton.Selected,
                src => Assert.Fail()));
        }
    }
}