#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using Moq;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Examples.Framework
{
    [TestFixture]
    public class PropertyBasedFinderBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldFindAnElementBasedOnAProperty()
        {
            var window = LaunchPetShopWindow();
            var basket = window.Find<Tab>(FindBy.WpfTitleOrText("Basket"));

            AutomationElement comboBoxElement = window.Element.FindFirst(TreeScope.Descendants,
               new PropertyCondition(AutomationElement.ClassNameProperty,
               typeof(ComboBox).Name));
            string comboBoxName = comboBoxElement.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty).ToString();

            var wrapperFactory = new Mock<IWrapAutomationElements>();
            var comboBox = new ComboBox(comboBoxElement, "aComboBox");
            wrapperFactory.Setup(x => x.Wrap<ComboBox>(comboBoxElement, comboBoxName)).Returns(comboBox);

            var finder = new PropertyBasedFinder(wrapperFactory.Object);
            finder.Find<ComboBox, Tab>(basket, FindBy.ControlType(ControlType.ComboBox), Assert.Fail);
        }
    }
}
