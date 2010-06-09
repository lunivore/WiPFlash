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
    public class NameBasedFinderBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldDetermineIfAnElementIsThereWithoutComplaining()
        {
            Window window = LaunchPetShopWindow();
            AutomationElement comboBoxElement = window.Element.FindFirst(TreeScope.Descendants,
               new PropertyCondition(AutomationElement.ClassNameProperty,
               typeof(ComboBox).Name));
            string comboBoxName = comboBoxElement.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty).ToString();

            var wrapperFactory = new Mock<IWrapAutomationElements>();
            
            var finder = new NameBasedFinder(wrapperFactory.Object);
            Assert.IsTrue(finder.Contains(window, comboBoxName));
        }

        [Test]
        public void ShouldFindAnElementFromTheGivenRootAndWrapUsingWrapper()
        {
            Window window = LaunchPetShopWindow();
            AutomationElement comboBoxElement = window.Element.FindFirst(TreeScope.Descendants,
               new PropertyCondition(AutomationElement.ClassNameProperty,
               typeof(ComboBox).Name));
            string comboBoxName = comboBoxElement.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty).ToString();

            var wrapperFactory = new Mock<IWrapAutomationElements>();
            var comboBox = new ComboBox(comboBoxElement, "aComboBox");
            wrapperFactory.Setup(x => x.Wrap<ComboBox>(comboBoxElement, comboBoxName)).Returns(comboBox);

            var finder = new NameBasedFinder(wrapperFactory.Object);
            Assert.AreEqual(comboBox, finder.Find<ComboBox, Window>(window, comboBoxName, Assert.Fail));
        }

        [Test]
        public void ShouldHandleElementNotFound()
        {
            Window window = LaunchPetShopWindow();
            var finder = new NameBasedFinder(new Mock<IWrapAutomationElements>().Object);
            var complained = false;
            finder.Find<ComboBox, Window>(window, "wibbleInput", (s) => complained = true);
            Assert.True(complained, "Should have complained about the non-existent element");
        }


    }
}
