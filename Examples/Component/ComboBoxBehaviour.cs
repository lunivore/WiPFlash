#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class ComboBoxBehaviour : AutomationElementWrapperExamples<ComboBox>
    {
        [Test]
        public void ShouldAllowValuesToBeSetWhenEditable()
        {
            ComboBox editableBox = CreateWrapper();
            editableBox.Select("");
            Assert.AreEqual("", editableBox.Selection);
            editableBox.Select("Rabbit");
            Assert.AreEqual("Rabbit", editableBox.Selection);
            editableBox.Select("WibbleBeast");
            Assert.AreEqual("WibbleBeast", editableBox.Selection);
        }

        [Test]
        public void ShouldAllowValuesToBeSetUsingToStringWhenNotEditable()
        {
            Window window = LaunchPetShopWindow();
            ComboBox nonEditableBox = window.Find<ComboBox>("petFoodInput");
            nonEditableBox.Select("");
            Assert.AreEqual("", nonEditableBox.Selection);
            nonEditableBox.Select("PetFood[Carnivorous]");
            Assert.AreEqual("PetFood[Carnivorous]", nonEditableBox.Selection);
        }

        protected override ComboBox CreateWrapperWith(AutomationElement element)
        {
            return new ComboBox(element);
        }

        protected override ComboBox CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            return window.Find<ComboBox>("petTypeInput");
        }
    }
}
