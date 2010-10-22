#region

using Example.PetShop.Scenarios.Steps;
using NUnit.Framework;

#endregion

namespace Example.PetShop.Scenarios.Utils
{
    public class PetShopScenario
    {
        private readonly PetShopSteps _petShopSteps;
        private readonly PetRegistrySteps _petRegistrySteps;
        private readonly BasketSteps _basketSteps;
        private readonly AccessoryRegistrySteps _accessoryRegistrySteps;
        private readonly HistorySteps _historySteps;
        private MessageBoxSteps _messageBoxSteps;

        protected PetShopScenario() : this(new Universe())
        {
            
        }

        private PetShopScenario(Universe universe) : this(new PetShopSteps(universe), new PetRegistrySteps(universe), new HistorySteps(universe), new BasketSteps(universe), new AccessoryRegistrySteps(universe), new MessageBoxSteps(universe))
        {
        }

        private PetShopScenario(PetShopSteps petShopSteps, PetRegistrySteps petRegistrySteps, HistorySteps historySteps, BasketSteps basketSteps, AccessoryRegistrySteps accessoryRegistrySteps, MessageBoxSteps messageBoxSteps)
        {
            _petShopSteps = petShopSteps;
            _petRegistrySteps = petRegistrySteps;
            _historySteps = historySteps;
            _basketSteps = basketSteps;
            _accessoryRegistrySteps = accessoryRegistrySteps;
            _messageBoxSteps = messageBoxSteps;
        }

        protected PetShopSteps GivenThePetshop
        {
            get { return _petShopSteps; }
        }

        protected PetRegistrySteps WhenAPetIsRegistered
        {
            get { return _petRegistrySteps; }
        }

        protected BasketSteps ThenTheBasket
        {
            get { return _basketSteps; }
        }

        protected HistorySteps ThenTheHistory
        {
            get { return _historySteps; }
        }

        protected HistorySteps AndTheHistory
        {
            get { return _historySteps; }
        }

        protected BasketSteps GivenTheBasket
        {
            get { return _basketSteps; }
        }

        protected BasketSteps WhenTheBasket
        {
            get { return _basketSteps; }
        }

        protected AccessoryRegistrySteps ThenTheAccessories
        {
            get { return _accessoryRegistrySteps; }
        }

        protected AccessoryRegistrySteps WhenTheAccessories
        {
            get { return _accessoryRegistrySteps; }
        }

        protected MessageBoxSteps ThenAMessageBox
        {
            get { return _messageBoxSteps; }
        }

        protected MessageBoxSteps WhenTheMessageBox
        {
            get { return _messageBoxSteps; }
        }

        [TearDown]
        public void CloseAllWindows()
        {
            _petShopSteps.CloseWindows();
        }
    }
}