﻿#region

using System.Windows.Automation;
using Moq;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Framework;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class WindowBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldUseTheFinderToDetermineIfItContainsAComponent()
        {
            var anElement = AutomationElement.RootElement;
            var finder = new Mock<IFindAutomationElements>();
            var window = new Window(anElement, finder.Object);

            finder.Setup(x => x.Contains(window, It.IsAny<PropertyCondition>())).Returns(true);
            Assert.IsTrue(window.Contains("here"));
            
            finder.Setup(x => x.Contains(window, It.IsAny<PropertyCondition>())).Returns(false);
            Assert.IsFalse(window.Contains("not.here"));

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

        [Test]
        public void ShouldPassFailureHandlerToChildContainers()
        {
            var container = LaunchPetShopWindow();
            var complained = true;
            container.HandlerForFailingToFind = s => complained = true;

            var childContainer = container.Find<Tab>(FindBy.WpfText("Basket"));
            childContainer.Find<ComboBox>("Unlikely!");
            Assert.True(complained, "Should have handled failure to find using the given handler");
        }
    }
}