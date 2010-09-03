using System.Collections.Generic;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Examples.ExampleUtils;
using WiPFlash.Framework;

namespace WiPFlash.Examples.Component
{
    [TestFixture]
    public class MenuBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldProvideSubMenus()
        {
            _window = LaunchPetShopWindow();
            var menu = _window.Find<Menu>("mainMenu");
            var subMenu = menu.Find<Menu>("tabMenu");
            var items = new List<string>(subMenu.Items);
            Assert.True(items.Contains("TabPresenter[History]"));
            Assert.True(items.Contains("TabPresenter[Accessories]"));
        }
    }
}