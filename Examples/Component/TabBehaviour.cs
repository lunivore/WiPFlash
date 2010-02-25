using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

namespace Examples.Component
{
    [TestFixture]
    public class TabBehaviour : AutomationElementWrapperExamples<Tab>
    {
        [Test]
        public void ShouldAllowTabToBeSelected()
        {
            Tab tab = FindPetShopElement("historyTab");

            Assert.False(tab.HasFocus());

            tab.Select();
            Assert.True(tab.HasFocus());
        }

        protected override Tab CreateWrapperWith(AutomationElement element)
        {
            return new Tab(element);
        }

        protected override Tab CreateWrapper()
        {
            return FindPetShopElement("historyTab");
        }
    }
}
