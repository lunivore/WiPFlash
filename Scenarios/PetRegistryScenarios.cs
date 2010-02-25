#region

using System;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Scenarios
{
    [TestFixture]
    public class PetRegistryScenarios : UIBasedExamples
    {
        [Test]
        public void ICanRegisterAPet()
        {
            Window window = LaunchPetShopWindow();
            window.Find<TextBox>("petNameInput").Text = "Snowdrop";
            window.Find<ComboBox>("petTypeInput").Select("PetType[Rabbit]");
            window.Find<ComboBox>("petFoodInput").Select("PetFood[Carnivorous]");
            window.Find<TextBox>("petPriceInput").Text = "100.00";
            window.Find<ListBox>("petRulesInput").Select("Rule[Dangerous]", "Rule[No Children]");
            window.Find<Button>("petSaveButton").Click();

            window.Find<Tab>("historyTab").Select();

            string expectedHistory = "Snowdrop the Rabbit registered at a price of £100.00. Food: Carnivorous";
            string actualHistory = window.Find<RichTextBox>("historyInput").Text;
            Assert.True(actualHistory.Contains(expectedHistory), 
                "Should have contained \r\n{0}\r\n but was :\r\n{1}", expectedHistory, actualHistory);
        }

    }
}
