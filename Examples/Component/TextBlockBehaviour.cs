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
    public class TextBlockBehaviour : AutomationElementWrapperExamples<TextBlock>
    {
        [Test]
        public void ShouldAllowTextToBeRetrievedFromBlock()
        {
            TextBlock block = CreateWrapper();
            Assert.AreEqual("History so far:" + Environment.NewLine, block.Text);
        }

        protected override TextBlock CreateWrapperWith(AutomationElement element)
        {
            return new TextBlock(element);
        }

        protected override TextBlock CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            window.Find<Tab>("historyTab").Select();
            return window.Find<TextBlock>("historyOutput");
        }
    }
}
