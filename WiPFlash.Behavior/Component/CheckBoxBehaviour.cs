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
    public class CheckBoxBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowItselfToBeToggled()
        {
            var checkBox = LaunchPetShopWindow().Find<CheckBox>("vatReceiptInput");
            Assert.False(checkBox.Selected);
            checkBox.Toggle();
            Assert.True(checkBox.Selected);
            checkBox.Toggle();
            Assert.False(checkBox.Selected);
        }

        [Test]
        public void ShouldAllowItselfToBeSelected()
        {
            var checkBox = LaunchPetShopWindow().Find<CheckBox>("vatReceiptInput");
            checkBox.Selected = true;
            Assert.True(checkBox.Selected);
            checkBox.Selected = false;
            Assert.False(checkBox.Selected);
        }

        [Test]
        public void ShouldBeAbleToWaitForSelection()
        {
            var checkBox = LaunchPetShopWindow().Find<CheckBox>("vatReceiptInput");
            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               checkBox.Selected = true;
                           }).Start(null);
            Assert.True(checkBox.WaitFor((src, e) => checkBox.Selected, (src) => Assert.Fail()));
        }
    }
}