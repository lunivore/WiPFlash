#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class EditableComboBoxBehaviour : ComboBoxBehaviour<EditableComboBox>
    {
        [Test]
        public void ShouldAllowValuesToBeSetWhenEditable()
        {
            EditableComboBox editableBox = CreateWrapper();
            editableBox.Select("");
            Assert.AreEqual("", editableBox.Selection);
            editableBox.Select("PetType[Rabbit]");
            Assert.AreEqual("PetType[Rabbit]", editableBox.Selection);
            editableBox.Text = "WibbleBeast";
            Assert.AreEqual("WibbleBeast", editableBox.Text);
        }

        protected override EditableComboBox CreateWrapperWith(AutomationElement element)
        {
            return new EditableComboBox(element);
        }

        protected override EditableComboBox CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            return window.Find<EditableComboBox>(ComboBoxName);
        }

        protected override string SelectableValue
        {
            get { return "PetType[Rabbit]"; }
        }

        protected override string ComboBoxName
        {
            get { return "petTypeInput"; }
        }
    }
}
