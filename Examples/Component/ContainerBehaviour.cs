#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Exceptions;

#endregion

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class ContainerBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldHandleFailureToFindAComponentByThrowingAnExceptionByDefault()
        {
            var container = new MyContainer();
            try
            {
                container.Find<ComboBox>("Wibble");
                Assert.Fail("Should have thrown an exception");
            }
            catch(FailureToFindException) { }
        }

        [Test]
        public void ShouldAllowTheFailureToFindHandlerToBeSet()
        {
            var container = new MyContainer();
            var complained = true;
            container.HandlerForFailingToFind = (s) => complained = true;
            container.Find<ComboBox>("Wibble");
            
            Assert.True(complained, "Should have handled failure to find using the given handler");
        }
    }

    public class MyContainer : Container
    {
        public MyContainer()
            : base(AutomationElement.RootElement)
        {
        }

        public MyContainer(AutomationElement e, string name)
            : base(e, name)
        {
        }
    }
}