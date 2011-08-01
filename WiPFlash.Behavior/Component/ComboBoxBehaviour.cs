#region

using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;
using WiPFlash.Exceptions;

#endregion

namespace WiPFlash.Behavior.Component
{
    [TestFixture]
    public abstract class ComboBoxBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldProvideCurrentItems()
        {
            Window window = LaunchPetShopWindow();
            ComboBox comboBox = window.Find<ComboBox>("petFoodInput");
            var items = new List<string>(comboBox.Items);
            Assert.True(items.Contains("PetFood[Carnivorous]"));
            Assert.True(items.Contains("PetFood[Eats People]"));
        }

        [Test]
        public void ShouldAllowValuesToBeSetUsingToStringWhenNotEditable()
        {
            var window = LaunchPetShopWindow();
            var nonEditableBox = window.Find<ComboBox>("petFoodInput");
            nonEditableBox.Select("");
            Assert.AreEqual("", nonEditableBox.Selection);
            nonEditableBox.Select("PetFood[Carnivorous]");
            Assert.AreEqual("PetFood[Carnivorous]", nonEditableBox.Selection);
        }

        [Test]
        public void ShouldComplainIfValueSelectedWhichIsntInTheList()
        {
            var window = LaunchPetShopWindow();
            var nonEditableBox = window.Find<ComboBox>("petFoodInput");
            try
            {
                nonEditableBox.Select("Wibble");
                Assert.Fail("Should have complained that it couldn't find the element Wibble");
            } 
            catch (FailureToFindException)
            {
                
            }
        }

        [Test]
        public void ShouldBeAbleToWaitForContentsToBeSelected()
        {
            var window = LaunchPetShopWindow();
            var nonEditableBox = window.Find<ComboBox>("petFoodInput");
            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               nonEditableBox.Select("PetFood[Carnivorous]");
                           }).Start();

            Assert.True(nonEditableBox.WaitFor(
                            (src, e) => nonEditableBox.Selection.Equals("PetFood[Carnivorous]"),
                            src => Assert.Fail()));
        }
    }
}