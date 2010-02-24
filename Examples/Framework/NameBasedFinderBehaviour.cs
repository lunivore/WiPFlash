#region

using System.Windows.Automation;
using Examples.ExampleUtils;
using Moq;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Examples.Framework
{
    [TestFixture]
    public class NameBasedFinderBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldFindAComponentFromTheGivenRootAndWrapUsingWrapper()
        {
            Window window = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH).FindWindow(EXAMPLE_APP_WINDOW_NAME);
            AutomationElement comboBoxElement = window.Element.FindFirst(TreeScope.Descendants,
               new PropertyCondition(AutomationElement.ClassNameProperty,
               typeof(ComboBox).Name));
            string comboBoxName = comboBoxElement.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty).ToString();

            var wrapper = new Mock<IWrapAutomationElements>();
            var comboBox = new ComboBox(comboBoxElement);
            wrapper.Setup(x => x.Wrap<ComboBox>(comboBoxElement)).Returns(comboBox);

            var finder = new NameBasedFinder(wrapper.Object);
            Assert.AreEqual(comboBox, finder.Find<ComboBox>(window, comboBoxName));

        }
    }
}
