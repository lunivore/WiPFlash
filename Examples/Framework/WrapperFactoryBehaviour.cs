﻿#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Examples.Framework
{
    [TestFixture]
    public class WrapperFactoryBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldWrapElementsUsingTheGivenTypeOfWrapper()
        {
            var wrapperFactory = new WrapperFactory();
            var anElement = AutomationElement.RootElement;

            Assert.IsAssignableFrom(typeof(TextBox), wrapperFactory.Wrap<TextBox>(anElement, "aTextBox"));
            Assert.IsAssignableFrom(typeof(RichTextBox), wrapperFactory.Wrap<RichTextBox>(anElement, "aRichTextBox"));
            Assert.IsAssignableFrom(typeof(ListBox), wrapperFactory.Wrap<ListBox>(anElement, "aListBox"));
            Assert.IsAssignableFrom(typeof(Button), wrapperFactory.Wrap<Button>(anElement, "aButton"));
            Assert.IsAssignableFrom(typeof(TextBlock), wrapperFactory.Wrap<TextBlock>(anElement, "aTextBlock"));
            Assert.IsAssignableFrom(typeof(Label), wrapperFactory.Wrap<Label>(anElement, "aLabel"));
            Assert.IsAssignableFrom(typeof(Tab), wrapperFactory.Wrap<Tab>(anElement, "aTab"));
            Assert.IsAssignableFrom(typeof(CheckBox), wrapperFactory.Wrap<CheckBox>(anElement, "aCheckBox"));
            Assert.IsAssignableFrom(typeof(RadioButton), wrapperFactory.Wrap<RadioButton>(anElement, "aRadioButton"));
        }

        [Test]
        public void ShouldWrapElementsToComboBoxesOrEditableComboBoxesAsAppropriate()
        {
            var wrapperFactory = new WrapperFactory();
            Window window = LaunchPetShopWindow();
            AutomationElement editableComboBox = window.Element.FindFirst(TreeScope.Descendants, 
                new PropertyCondition(AutomationElement.AutomationIdProperty, "petTypeInput"));
            AutomationElement comboBox = window.Element.FindFirst(TreeScope.Descendants,
                 new PropertyCondition(AutomationElement.AutomationIdProperty, "petFoodInput"));

            Assert.IsAssignableFrom(typeof(EditableComboBox), wrapperFactory.Wrap<ComboBox>(editableComboBox, "anEditableComboBox"));
            Assert.IsAssignableFrom(typeof(ComboBox), wrapperFactory.Wrap<ComboBox>(comboBox, "aNormalComboBox"));
        }
    }
}
