#region

using System;
using System.Collections.Generic;
using Example.PetShop.Scenarios.Utils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

#endregion

namespace Example.PetShop.Scenarios.Steps
{
    public class BasketSteps
    {
        private readonly Universe _universe;

        public BasketSteps(Universe universe)
        {
            _universe = universe;
        }

        public void ShouldList(string name)
        {
            _universe.Window.Find<ComboBox>("basketPetInput").WaitFor(
                (cb, e) => new List<string>(((ComboBox)cb).Items).Contains("Pet[" + name + "]"),
                e => Assert.Fail("Combo box should have contained a pet with name {0}", name));
        }

        public void IsAddedWith(string name)
        {
            _universe.Window.Find<ComboBox>("basketPetInput").Select("Pet[" + name + "]");
        }

        public void ShouldHaveTotal(double expectedTotal)
        {
            string expectedTotalAsString = expectedTotal.ToString("0.00");
            string actualTotal = _universe.Window.Find<Label>("totalOutput").Text;
            Assert.AreEqual(expectedTotalAsString, actualTotal);
        }

        public BasketSteps IsACardPayment()
        {
            _universe.Window.Find<RadioButton>("cardPaymentInput").Select();

            return this;
        }

        public BasketSteps RequiresAVATReceipt()
        {
            _universe.Window.Find<CheckBox>("vatReceiptInput").Toggle();
            return this;
        }

        public void IsPurchased()
        {
            _universe.Window.Find<Button>("purchaseButton").Click();
        }

        public void ShouldNotList(string name)
        {
            string[] goodsAvailable = _universe.Window.Find<ComboBox>("basketPetInput").Items;
            Assert.False(new List<string>(goodsAvailable).Contains("Pet[" + name + "]"));
        }

        public void ShouldContain(string name, double price)
        {
            _universe.Window.Find<Tab>(FindBy.WpfText("Basket")).Select();
            var basketContents = _universe.Window.Find<GridView>("basketOutput");
            Assert.True(basketContents.ContainsRow(name, price.ToString("0.00")));
        }

        public void IsReset()
        {
            _universe.Window.Find<Tab>(FindBy.WpfText("Basket")).Select();
            _universe.Window.Find<Button>("resetButton").Click();
        }

        public void ShouldBeEmpty()
        {
            _universe.Window.Find<Tab>(FindBy.WpfText("Basket")).Select();
            var basketContents = _universe.Window.Find<GridView>("basketOutput");
            Assert.IsTrue(basketContents.IsEmpty());
        }

        public void IsResetSuccessfully()
        {
            IsReset();
            _universe.MessageBoxSteps.IsConfirmed();
        }
    }
}