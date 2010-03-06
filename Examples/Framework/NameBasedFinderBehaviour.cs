#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using Moq;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Exceptions;
using WiPFlash.Framework;

#endregion

namespace Examples.Framework
{
    [TestFixture]
    public class NameBasedFinderBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldFindAnElementFromTheGivenRootAndWrapUsingWrapper()
        {
            Window window = LaunchPetShopWindow();
            AutomationElement comboBoxElement = window.Element.FindFirst(TreeScope.Descendants,
               new PropertyCondition(AutomationElement.ClassNameProperty,
               typeof(ComboBox).Name));
            string comboBoxName = comboBoxElement.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty).ToString();

            var wrapperFactory = new Mock<IWrapAutomationElements>();
            var comboBox = new ComboBox(comboBoxElement);
            wrapperFactory.Setup(x => x.Wrap<ComboBox>(comboBoxElement)).Returns(comboBox);

            var finder = new NameBasedFinder(wrapperFactory.Object);
            Assert.AreEqual(comboBox, finder.Find<ComboBox>(window, comboBoxName));
        }

        [Test]
        public void ShouldComplainIfElementNotFound()
        {
            Window window = LaunchPetShopWindow();
            var finder = new NameBasedFinder(new Mock<IWrapAutomationElements>().Object);
            try
            {
                finder.Find<ComboBox>(window, "wibbleInput");
                Assert.Fail("Should have complained about the non-existent element");
            }
            catch(FailureToFindException) {}
        }


    }
}
