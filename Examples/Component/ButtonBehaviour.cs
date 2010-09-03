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
    public class ButtonBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldBeClickable()
        {
            Window window = LaunchPetShopWindow();
            var button = window.Find<Button>("petSaveButton");
            button.Click();
        }

        [Test]
        public void ShouldProvideItsText()
        {
            Window window = LaunchPetShopWindow();
            var button = window.Find<Button>("petSaveButton");
            Assert.AreEqual("Save", button.Text);
        }

        [Test]
        public void ShouldBeAbleToWaitUntilItsEnabled()
        {
            Window window = LaunchPetShopWindow();
            var petsToBuy = window.Find<ComboBox>("basketPetInput");
            var button = window.Find<Button>("petSaveButton");

            new Thread(o => {
                Thread.Sleep(100); 
                petsToBuy.Select("Pet[Dancer]");
            }).Start(null);

            button.WaitFor((src, e) => button.IsEnabled, src => Assert.Fail());
        }
    }
}