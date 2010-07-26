#region

using System.Windows.Automation;
using Example.PetShop.Scenarios.Utils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;
using WiPFlash.Util;

#endregion

namespace Example.PetShop.Scenarios.Steps
{
    public class AccessoryRegistrySteps
    {
        private readonly Universe _universe;

        public AccessoryRegistrySteps(Universe universe)
        {
            _universe = universe;
        }

        public void AreSelected(params string[] accessoryNames)
        {
            var accessoriesTab = _universe.Window.Find<Tab>(FindBy.WpfText("Accessories"));
            accessoriesTab.Select();
            var scrollViewer =
                accessoriesTab.Find<ScrollViewer>(new PropertyCondition(AutomationElement.IsScrollPatternAvailableProperty, true));

            var table = accessoriesTab.Find<ListBox>("accessoriesOutput");

            foreach (var name in accessoryNames)
            {
                var nameForThisLoop = name;
                scrollViewer.ScrollDown(s => s.Contains(nameForThisLoop),s => {});
                scrollViewer.ScrollUp(s => s.Contains(FindBy.WpfText(nameForThisLoop)), (s2) => Assert.Fail("Should have scrolled to {0}" + nameForThisLoop));
            }
            table.Select(CollectionUtils.Convert(accessoryNames, name=> "Accessory[" + name + "]").ToArray());
        }
    }
}