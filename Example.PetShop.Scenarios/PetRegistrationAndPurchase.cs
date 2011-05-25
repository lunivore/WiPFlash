#region

using Example.PetShop.Scenarios.Utils;
using NUnit.Framework;

#endregion

namespace Example.PetShop.Scenarios
{
    [TestFixture]
    public class PetRegistrationAndPurchase : PetShopScenario
    {
        [Test]
        public void ICanRegisterNewPetsForSale()
        {
            GivenThePetshop.IsRunning();
            WhenAPetIsRegistered.WithName("Snowdrop")
                .WithType("Rabbit")
                .WhoEats("Carnivorous")
                .WhoHasRules("Dangerous", "No Children")
                .AtAPrice("100.00")
                .AndSaved();
            ThenTheHistory.ShouldContain("Snowdrop the Rabbit registered at a price of £100.00. Food: Carnivorous");
            AndTheHistory.ShouldIncludeMostRecentPet("Snowdrop");
            ThenTheBasket.ShouldList("Snowdrop");           
        }

        [Test]
        public void CustomersCanPurchaseAPet()
        {
            GivenThePetshop.IsRunning();
            WhenTheBasket.IsAddedWith("Dancer");
            ThenTheBasket.ShouldNotList("Dancer");
            ThenTheBasket.ShouldContain("Dancer", "54.00");
            ThenTheBasket.ShouldHaveTotal("54.00");
            WhenTheBasket.IsACardPayment()
                .RequiresAVATReceipt()
                .IsPurchased();
            ThenTheBasket.ShouldNotList("Dancer");
        }

        [Test]
        public void CustomersCanPurchaseAccessories()
        {
            GivenThePetshop.IsRunning();
            WhenTheAccessories.AreSelected("Rubber bone", "Dog Collar (Large)", "Dog Collar (Small)");
            ThenTheBasket.ShouldContain("Rubber bone", "1.50");
            ThenTheBasket.ShouldContain("Dog Collar (Large)", "10.00");
            ThenTheBasket.ShouldContain("Dog Collar (Small)", "9.00");
        }

        [Test]
        public void ICanResetTheBasket()
        {
            GivenThePetshop.IsRunning();
            WhenTheAccessories.AreSelected("Rubber bone", "Dog Collar (Large)", "Dog Collar (Small)");
            WhenTheBasket.IsResetSuccessfully();
            ThenTheBasket.ShouldBeEmpty();

        }

        [Test]
        public void IAmPreventedFromClearingCustomersBasketsAccidentally()
        {
            GivenThePetshop.IsRunning();
            GivenTheBasket.IsAddedWith("Spot");
            WhenTheBasket.IsReset();
            ThenAMessageBox.ShouldAskUs("Are you sure you want to clear the contents of the basket?");
            WhenTheMessageBox.IsDeclined();
            ThenTheBasket.ShouldContain("Spot", "100.00");
        }
    }
}