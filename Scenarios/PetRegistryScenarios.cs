#region

using System.Collections.Generic;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

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

            window.Find<Tab>(new TitleBasedFinder(), "History").Select();

            string expectedHistory = "Snowdrop the Rabbit registered at a price of £100.00. Food: Carnivorous";
            var historyInput = window.Find<RichTextBox>("historyInput");
            historyInput.WaitFor(hi => hi.Text.Contains(expectedHistory));

            
            Assert.True(historyInput.Text.Contains(expectedHistory), 
                "Should have contained \r\n{0}\r\n but was :\r\n{1}", expectedHistory, historyInput.Text);

            var view = window.Find<GridView>("lastPetsOutput");
            Assert.AreEqual(view.TextAt(0, 2), "Snowdrop");

            window.Find<ComboBox>("basketInput").WaitFor(cb => new List<string>(cb.Items).Contains("Pet[Snowdrop]"));

            window.Find<ComboBox>("basketInput").Select("Pet[Snowdrop]");
            string actualTotal = window.Find<Label>("totalOutput").Text;
            Assert.AreEqual("100.00", actualTotal);

            var basketContents = window.Find<GridView>("basketOutput");
            Assert.AreEqual("Snowdrop", basketContents.TextAt(0, 0));
            Assert.AreEqual("100.00", basketContents.TextAt(1, 0));

            window.Find<RadioButton>("cardPaymentInput").Select();
            window.Find<CheckBox>("vatReceiptInput").Toggle();
            window.Find<Button>("purchaseButton").Click();

            string[] goodsAvailable = window.Find<ComboBox>("basketInput").Items;
            Assert.False(new List<string>(goodsAvailable).Contains("Pet[Snowdrop]"));
        }

    }
}
