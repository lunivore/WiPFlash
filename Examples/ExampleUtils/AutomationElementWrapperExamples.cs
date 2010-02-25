#region

using System;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Examples.ExampleUtils
{
    public abstract class AutomationElementWrapperExamples<T> : UIBasedExamples where T : AutomationElementWrapper
    {
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
                CreateWrapperWith(null);
                Assert.Fail("AutomationElementWrappers should fail at the point of being initialised with null");
            } catch (NullReferenceException){}
        }

        protected abstract T CreateWrapperWith(AutomationElement element);

        protected abstract T CreateWrapper();

        protected T FindPetShopElement(string name)
        {
            Window window = LaunchPetShopWindow();
            return window.Find<T>(name);
        }
    }
}
