#region

using System;
using System.Windows.Automation;
using Example.PetShop.Scenarios.Utils;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Example.PetShop.Scenarios.Steps
{
    public class PetRegistrySteps
    {
        private readonly Universe _universe;

        public PetRegistrySteps(Universe universe)
        {
            _universe = universe;
        }

        public PetRegistrySteps WithName(string name)
        {
            _universe.Window.Find<TextBox>("petNameInput").Text = name;
            return this;
        }

        public PetRegistrySteps WithType(string petType)
        {
            _universe.Window.Find<ComboBox>("petTypeInput").Select("PetType[" + petType + "]");
            return this;
        }

        public PetRegistrySteps WhoEats(string food)
        {
            _universe.Window.Find<ComboBox>("petFoodInput").Select("PetFood[" + food + "]");
            return this;
        }

        public PetRegistrySteps WhoHasRules(params string[] rules)
        {
            foreach (var rule in rules)
            {
                _universe.Window.Find<ListBox>("petRulesInput").Select("Rule[" + rule + "]");
            }
            return this;
        }


        public PetRegistrySteps AtAPrice(double price)
        {
            _universe.Window.Find<TextBox>("petPriceInput").Text = price.ToString("0.00");
            return this;
        }

        public void AndSaved()
        {
            _universe.Window.Find<Button>("petSaveButton").Click();
        }

        public PetRegistrySteps ByCopying(string name)
        {
            // Need to click *off* the current text box to save the details
            // This is a Microsoft WPF thing, not a WiPFlash thing.
            _universe.Window.Find<TextBox>("petPriceInput").Element.SetFocus();
            _universe.Window.Find<TextBox>("petNameInput").Element.SetFocus();
            _universe.Window.Find<Label>("copyPetContextTarget")
                .InvokeContextMenu(FindBy.WpfName("copyPetMenu"))
                .Select("Pet[" + name + "]");
            return this;
        }
    }
}