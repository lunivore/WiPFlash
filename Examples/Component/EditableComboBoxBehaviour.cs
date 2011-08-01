#region

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
    public class EditableComboBoxBehaviour: UIBasedExamples
    {
        [Test]
        public void ShouldAllowValuesToBeSetWhenEditable()
        {
            var editableBox = LaunchPetShopWindow().Find<EditableComboBox>("petTypeInput");
            editableBox.Select("");
            Assert.AreEqual("", editableBox.Selection);
            editableBox.Select("PetType[Rabbit]");
            Assert.AreEqual("PetType[Rabbit]", editableBox.Selection);
            editableBox.Text = "WibbleBeast";
            Assert.AreEqual("WibbleBeast", editableBox.Text);
        }

        [Test]
        public void ShouldProvideCurrentItems()
        {
            Window window = LaunchPetShopWindow();
            var comboBox = window.Find<EditableComboBox>("petTypeInput");
            var items = new System.Collections.Generic.List<string>(comboBox.Items);
            Assert.True(items.Contains("PetType[Rabbit]"));
            Assert.True(items.Contains("PetType[Dog]"));
        }

        [Test]
        public void ShouldAllowValuesToBeSetUsingToString()
        {
            var window = LaunchPetShopWindow();
            var editableBox = window.Find<EditableComboBox>("petTypeInput");
            editableBox.Select("");
            Assert.AreEqual("", editableBox.Selection);
            editableBox.Select("PetType[Rabbit]");
            Assert.AreEqual("PetType[Rabbit]", editableBox.Selection);
        }

        [Test]
        public void ShouldComplainIfValueSelectedWhichIsntInTheList()
        {
            var window = LaunchPetShopWindow();
            var nonEditableBox = window.Find<EditableComboBox>("petTypeInput");
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
            var nonEditableBox = window.Find<EditableComboBox>("petTypeInput");
            new Thread(o =>
                           {
                               Thread.Sleep(100);
                               nonEditableBox.Select("PetType[Rabbit]");
                           }).Start();

            Assert.True(nonEditableBox.WaitFor(
                            (src, e) => nonEditableBox.Selection.Equals("PetType[Rabbit]"), 
                            src => Assert.Fail()));
        }

    }
}