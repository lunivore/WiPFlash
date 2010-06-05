using System;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Exceptions;

namespace Examples.Component
{
    [TestFixture]
    public class ContainerExamples : AutomationElementWrapperExamples<MyContainer>
    {
        [Test]
        public void ShouldHandleFailureToFindAComponentByThrowingAnExceptionByDefault()
        {
            var container = CreateWrapper();
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
            var container = CreateWrapper();
            var complained = true;
            container.HandlerForFailingToFind = (s) => complained = true;
            container.Find<ComboBox>("Wibble");
            
            Assert.True(complained, "Should have handled failure to find using the given handler");
        }

        protected override MyContainer CreateWrapperWith(AutomationElement element, string name)
        {
            return new MyContainer(element, name);
        }

        protected override MyContainer CreateWrapper()
        {
            return new MyContainer();
        }
    }

    public class MyContainer : Container<MyContainer>
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