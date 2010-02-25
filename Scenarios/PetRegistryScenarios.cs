#region

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
            window.Find<ComboBox>("petTypeInput").Select("Rabbit");
            window.Find<ComboBox>("petTypeInput").Select("PetFood[Carnivorous]");
            window.Find<TextBox>("petPriceInput").Text = "100.00";
            window.Find<ListBox>("petRulesInput").Select("Rule[Dangerous]", "Rule[No Children]");
            window.Find<Button>("petSaveButton").Click();

            window.Find<Tab>("historyTab").Select();

            Assert.True(window.Find<RichTextBox>("historyInput").Text.Contains("Snowdrop the Rabbit registered at a price of £100"));
        }

    }
}
