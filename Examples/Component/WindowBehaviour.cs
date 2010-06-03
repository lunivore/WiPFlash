#region

using System.ComponentModel;
using System.Windows.Automation;
using Examples.ExampleUtils;
using Moq;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class WindowBehaviour : AutomationElementWrapperExamples<Window>
    {
        [Test]
        public void ShouldUseTheFinderToDetermineIfItContainsAComponent()
        {
            var anElement = AutomationElement.RootElement;

            var finder = new Mock<IFindAutomationElements>();

            var window = new Window(anElement, finder.Object);

            finder.Setup(x => x.Contains(window, "here")).Returns(true);
            finder.Setup(x => x.Contains(window, "not.here")).Returns(false);

            Assert.IsTrue(window.Contains("here"));
            Assert.IsFalse(window.Contains("not.here"));

        }

        [Test]
        public void ShouldFindTheComponentUsingTheFinderWithItselfAsRoot()
        {
            var anElement = AutomationElement.RootElement;
            var textBox = new TextBox(anElement, "aTextInput");
            var comboBox = new ComboBox(anElement, "aComboInput");

            var finder = new Mock<IFindAutomationElements>();

            var window = new Window(anElement, finder.Object);

            finder.Setup(x => x.Find<TextBox, Window>(window, "aTextInput")).Returns(textBox);
            finder.Setup(x => x.Find<ComboBox, Window>(window, "aComboInput")).Returns(comboBox);

            Assert.AreEqual(textBox, window.Find<TextBox>("aTextInput"));
            Assert.AreEqual(comboBox, window.Find<ComboBox>("aComboInput"));
        }

        [Test]
        public void ShouldBeAbleToWaitForWindowEvents()
        {
            GivenThisWillHappenAtSomePoint(window => ((Window)window).Close());
            ThenWeShouldBeAbleToWaitFor(window => ((Window)window).IsClosed());
        }

        [Test]
        public void ShouldAllowItselfToBeClosed()
        {
            CloseAllExampleApplications();

            Window window = LaunchPetShopWindow();
            int processId = int.Parse(window.Element.GetCurrentPropertyValue(AutomationElement.ProcessIdProperty).ToString());
            window.Close();

            var windows = AutomationElement.RootElement.FindAll(TreeScope.Descendants,
                                                  new AndCondition(
                                                      new PropertyCondition(AutomationElement.AutomationIdProperty,
                                                                        EXAMPLE_APP_WINDOW_NAME),
                                                      new PropertyCondition(AutomationElement.ProcessIdProperty,
                                                          processId)));
            Assert.AreEqual(0, windows.Count);
        }

        protected override Window CreateWrapperWith(AutomationElement element, string name)
        {
            return new Window(element, name);
        }

        protected override Window CreateWrapper()
        {
            return LaunchPetShopWindow();
        }
    }
}
