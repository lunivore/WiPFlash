using System;
using Example.PetShop.Scenarios.Utils;
using NUnit.Framework;
using WiPFlash.Components;

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


        public PetRegistrySteps AtAPrice(string price)
        {
            _universe.Window.Find<TextBox>("petPriceInput").Text = price;
            return this;
        }

        public void AndSaved()
        {
            _universe.Window.Find<Button>("petSaveButton").Click();
        }
    }
}