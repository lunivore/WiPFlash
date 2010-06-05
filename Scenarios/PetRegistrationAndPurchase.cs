#region

using System;
using System.Collections.Generic;
using Examples.ExampleUtils;
using NUnit.Framework;
using Scenarios.Utils;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Scenarios
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
    }
}
