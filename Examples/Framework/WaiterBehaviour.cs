using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using Moq;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Framework;
using WiPFlash.Framework.Events;

namespace WiPFlash.Examples.Framework
{
    [TestFixture]
    public class WaiterBehaviour : UIBasedExamples
    {
        [Test]
        public void shouldWaitForEventsToOccur()
        {
            // Given an automation element
            _window = LaunchPetShopWindow();
            var combo = _window.Find<ComboBox>("petFoodInput");

            // When we cause a slow event on that element            
            new Thread(() =>
            {
                Thread.Sleep(200);
                combo.Select("PetFood[Carnivorous]");
            }).Start();

            // And we wait for the event
            var eventOccurred = false;
            new Waiter().WaitFor(combo, (src, e) =>
                              {
                                  eventOccurred = true;
                                  return combo.Selection.Equals("PetFood[Carnivorous]");
                              }, new TimeSpan(0, 0, 1),
                          (src) => Assert.Fail(), new List<AutomationEventWrapper> {new StructureChangeEvent(TreeScope.Element)});

            // Then we should be notified when the event occurs
            Assert.IsTrue(eventOccurred);
        }

        [Test]
        public void shouldOnlyRunPotentiallyExpensiveChecksOnceIfSuccessful()
        {                     
            // When the waiter waits for a check which is already true
            var checkProvider = new Mock<CheckProvider>();
            checkProvider.Setup(cp => cp.Check()).Returns(true);

            new Waiter().WaitFor(new Panel(AutomationElement.RootElement, "Desktop"),
                (src, e) => checkProvider.Object.Check(),
                new TimeSpan(0, 0, 5),
                (src) => Assert.Fail("Could not handle guaranteed event"),
                new List<AutomationEventWrapper>());

            // Then it should only have run once
            checkProvider.Verify(cp => cp.Check(), Times.AtMostOnce());
        }

        public interface CheckProvider
        {
            bool Check();
        }
    }
}
