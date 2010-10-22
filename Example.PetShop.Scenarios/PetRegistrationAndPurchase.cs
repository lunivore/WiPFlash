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
        public void ICanRegisterAPet()
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
            WhenTheBasket.IsAddedWith("Snowdrop");
            ThenTheBasket.ShouldContain("Snowdrop", "100.00");
            ThenTheBasket.ShouldHaveTotal("100.00");
            WhenTheBasket.IsACardPayment()
                .RequiresAVATReceipt()
                .IsPurchased();
            ThenTheBasket.ShouldNotList("Snowdrop");
        }

        [Test]
        public void ICanBrowseAccessories()
        {
            GivenThePetshop.IsRunning();
            WhenTheAccessories.AreSelected("Rubber bone", "Dog Collar (Large)", "Dog Collar (Small)");
            ThenTheBasket.ShouldContain("Rubber bone", "1.50");
            ThenTheBasket.ShouldContain("Dog Collar (Large)", "10.00");
            ThenTheBasket.ShouldContain("Dog Collar (Small)", "9.00");
        }

        [Test]
        public void IAmAskedToConfirmBeforeClearingABasketOfGoods()
        {
            GivenThePetshop.IsRunning();
            WhenTheBasket.IsAddedWith("Snowdrop");
        }
    }
}