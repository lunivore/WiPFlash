﻿#region

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
                .AtAPrice(100.00)
                .AndSaved();
            ThenTheHistory.ShouldContain(100.00, "Snowdrop", "Rabbit", "Carnivorous");
            AndTheHistory.ShouldIncludeMostRecentPet("Snowdrop");
            ThenTheBasket.ShouldList("Snowdrop");           
        }

        [Test]
        public void CustomersCanPurchasePetsAndAccessories()
        {
            GivenThePetshop.IsRunning();
            WhenTheBasket.IsAddedWith("Dancer");
            ThenTheBasket.ShouldNotList("Dancer");
            ThenTheBasket.ShouldContain("Dancer", 54.00);
            ThenTheBasket.ShouldHaveTotal(54.00);

            WhenTheAccessories.AreSelected("Rubber bone", "Dog Collar (Large)", "Dog Collar (Small)");
            ThenTheBasket.ShouldContain("Rubber bone", 1.50);
            ThenTheBasket.ShouldContain("Dog Collar (Large)", 10.00);
            ThenTheBasket.ShouldContain("Dog Collar (Small)", 9.00);
            ThenTheBasket.ShouldHaveTotal(74.50);

            WhenTheBasket.RequiresAVATReceipt()
                .IsPurchased();
            ThenTheBasket.ShouldBeEmpty();
            ThenTheBasket.ShouldHaveTotal(0.00);
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
            ThenTheBasket.ShouldContain("Spot", 100.00);
        }

        [Test]
        public void ICanCopyAnExistingPetsDetails()
        {
            GivenThePetshop.IsRunning();
            WhenAPetIsRegistered.WithName("Fluffy")
                .ByCopying("Spot")
                .AndSaved();
            ThenTheBasket.ShouldList("Fluffy");
            WhenTheBasket.IsAddedWith("Fluffy");
            ThenTheBasket.ShouldContain("Fluffy", 100.00);

            WhenAPetIsRegistered.WithName("Nutmeg")
                .ByCopying("Fluffy")
                .AndSaved();
            ThenTheBasket.ShouldList("Nutmeg");
        }
    }
}