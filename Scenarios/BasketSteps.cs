using System;
using System.Collections.Generic;
using NUnit.Framework;
using WiPFlash.Components;

namespace Scenarios
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
            _universe.Window.Find<ComboBox>("basketInput").WaitFor(cb => new List<string>(cb.Items).Contains("Pet[" + name + "]"));
        }

        public void IsAddedWith(string name)
        {
            _universe.Window.Find<ComboBox>("basketInput").Select("Pet[" + name + "]");
        }

        public void ShouldHaveTotal(string expectedTotal)
        {
            string actualTotal = _universe.Window.Find<Label>("totalOutput").Text;
            Assert.AreEqual(expectedTotal, actualTotal);
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
            string[] goodsAvailable = _universe.Window.Find<ComboBox>("basketInput").Items;
            Assert.False(new List<string>(goodsAvailable).Contains("Pet[" + name + "]"));
        }

        public void ShouldContain(string name, string price)
        {
            var basketContents = _universe.Window.Find<GridView>("basketOutput");
            Assert.AreEqual(name, basketContents.TextAt(0, 0));
            Assert.AreEqual(price, basketContents.TextAt(1, 0));
        }
    }
}