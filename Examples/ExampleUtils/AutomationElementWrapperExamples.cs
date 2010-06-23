#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework.Events;

#endregion

namespace Examples.ExampleUtils
{
    public abstract class AutomationElementWrapperExamples<T> : UIBasedExamples where T : AutomationElementWrapper<T>
    {
        private T _element;

        protected delegate void ActionToPerform(T element);

        protected void GivenThisWillHappenAtSomePoint(ActionToPerform action)
        {
            _element = CreateWrapper();
            new Thread(() =>
                           {
                               Thread.Sleep(200);
                               action(_element);
                           }).Start();
        }

        protected void ThenWeShouldBeAbleToWaitFor(AutomationElementWrapper<T>.SomethingToWaitFor check)
        {
            _element.WaitFor(check, TimeSpan.Parse("00:00:01"), e => Assert.Fail("Should have waited successfully"));
            Assert.True(check(_element));
        }

        [Test]
        public void ShouldHandleFailureOfSomethingToHappen()
        {
            var wrapper = CreateWrapper();
            var handled = false;
            wrapper.WaitFor(w => false, TimeSpan.Parse("00:00:00"), w => handled = true);
            Assert.True(handled, "Should have handled being unable to wait for false be true");
        }

        [Test]
        public void ShouldProvideAccessToTheUnderlyingAutomationElement()
        {
            T wrapper = CreateWrapper();
            Assert.IsNotNull(wrapper.Element);
        }

        [Test]
        public void ShouldComplainIfInitialisedWithNull()
        {
            try
            {
                CreateWrapperWith(null, string.Empty);
                Assert.Fail("AutomationElementWrappers should fail at the point of being initialised with null");
            } catch (NullReferenceException){}
        }

        [Test]
        public void ShouldProvideTheNameWithWhichItWasConstructed()
        {
            T wrapper = CreateWrapperWith(AutomationElement.RootElement, "nameOfElement");
            Assert.AreEqual("nameOfElement", wrapper.Name);
        }

        protected abstract T CreateWrapperWith(AutomationElement element, string name);

        protected abstract T CreateWrapper();

        protected Window PetShopWindow { get { return _window; }}

        protected T FindPetShopElement(string name)
        {
            _window = LaunchPetShopWindow();
            _window.HandlerForFailingToFind = Assert.Fail;
            return _window.Find<T>(name);
        }
    }
}
