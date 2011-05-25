#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using Moq;
using NUnit.Framework;
using WiPFlash.Behavior.ExampleUtils;
using WiPFlash.Components;
using WiPFlash.Framework;
using WiPFlash.Framework.Events;

#endregion

namespace WiPFlash.Behavior.Component
{
    [TestFixture]
    public class AutomationElementWrapperBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldAllowTheWaiterToWaitForEventsToHappen()
        {
            // Given an element and a waiter
            var waiter = new Mock<IWaitForEvents>();
            var element = new StubAutomationElementWrapper(AutomationElement.RootElement, "Desktop", waiter.Object);

            // When we wait for an event successfully
            waiter.Setup(w => w.WaitFor(
                                  It.IsAny<AutomationElementWrapper>(),
                                  It.IsAny<SomethingToWaitFor>(),
                                  It.IsAny<TimeSpan>(),
                                  It.IsAny<FailureToHappenHandler>(),
                                  It.IsAny<IEnumerable<AutomationEventWrapper>>())).Returns(true);
            var result = element.WaitFor((src, e) => true, new TimeSpan(0, 0, 1), (src) => Assert.Fail());

            // Then we should be successful
            Assert.IsTrue(result);

            // When the event fails
            waiter.Setup(w => w.WaitFor(
                                  It.IsAny<AutomationElementWrapper>(),
                                  It.IsAny<SomethingToWaitFor>(),
                                  It.IsAny<TimeSpan>(),
                                  It.IsAny<FailureToHappenHandler>(),
                                  It.IsAny<IEnumerable<AutomationEventWrapper>>())).Returns(false);
            result = element.WaitFor((src, e) => false, new TimeSpan(0, 0, 1), (src) => Assert.Fail());

            // Then we should know
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldProvideAccessToTheUnderlyingAutomationElement()
        {
            var element = new StubAutomationElementWrapper(AutomationElement.RootElement, "Desktop");
            Assert.AreEqual(AutomationElement.RootElement, element.Element);
        }

        [Test]
        public void ShouldComplainIfInitialisedWithNull()
        {
            try
            {
                new StubAutomationElementWrapper(null, "An empty element");
                Assert.Fail("AutomationElementWrappers should fail at the point of being initialised with null");
            } catch (NullReferenceException){}
        }

        [Test]
        public void ShouldProvideTheNameWithWhichItWasConstructed()
        {
            var element = new StubAutomationElementWrapper(AutomationElement.RootElement, "nameOfElement");
            Assert.AreEqual("nameOfElement", element.Name);
        }       

        [Test]
        public void ShouldDetermineIfAnElementIsEnabled()
        {
            var window = LaunchPetShopWindow();
            Assert.IsFalse(window.Find<Button>("resetButton").IsEnabled);
            Assert.IsTrue(window.Find<Button>("petSaveButton").IsEnabled);
        }
    }

    class StubAutomationElementWrapper : AutomationElementWrapper
    {
        public StubAutomationElementWrapper(AutomationElement element, string name) : base(element, name)
        {
        }

        public StubAutomationElementWrapper(AutomationElement element, string name, IWaitForEvents waiter) : base(element, name, waiter)
        {
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get { return new List<AutomationEventWrapper>{ new FocusEvent() } ; }
        }
    }
}