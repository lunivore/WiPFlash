#region

using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Examples.Framework
{
    [TestFixture]
    public class WrapperFactoryBehaviour
    {
        [Test]
        public void ShouldWrapElementsUsingTheGivenTypeOfWrapper()
        {
            var wrapperFactory = new WrapperFactory();
            var anElement = AutomationElement.RootElement;

            Assert.IsAssignableFrom(typeof(TextBox), wrapperFactory.Wrap<TextBox>(anElement));
            Assert.IsAssignableFrom(typeof(RichTextBox), wrapperFactory.Wrap<RichTextBox>(anElement));
            Assert.IsAssignableFrom(typeof(ComboBox), wrapperFactory.Wrap<ComboBox>(anElement));
            Assert.IsAssignableFrom(typeof(ListBox), wrapperFactory.Wrap<ListBox>(anElement));
            Assert.IsAssignableFrom(typeof(Button), wrapperFactory.Wrap<Button>(anElement));
            Assert.IsAssignableFrom(typeof(Tab), wrapperFactory.Wrap<Tab>(anElement));
        }
    }
}
