#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class EditableComboBoxBehaviour : ComboBoxBehaviour
    {
        [Test]
        public void ShouldAllowValuesToBeSetWhenEditable()
        {
            var editableBox = (EditableComboBox)CreateWrapper();
            editableBox.Select("");
            Assert.AreEqual("", editableBox.Selection);
            editableBox.Select("PetType[Rabbit]");
            Assert.AreEqual("PetType[Rabbit]", editableBox.Selection);
            editableBox.Text = "WibbleBeast";
            Assert.AreEqual("WibbleBeast", editableBox.Text);
        }

        [Test]
        public void ShouldAllowMeToWaitUntilTheSelectionOrTheItemsChange()
        {
            GivenThisWillHappenAtSomePoint(combo => combo.Select("PetType[Rabbit]"));
            ThenWeShouldBeAbleToWaitFor((cb) => cb.Selection.Equals("PetType[Rabbit]"));
        }

        protected override ComboBox CreateWrapperWith(AutomationElement element, string name)
        {
            return new EditableComboBox(element, name);
        }

        protected override ComboBox CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            return window.Find<ComboBox>(ComboBoxName);
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