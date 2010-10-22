#region

using Example.PetShop.Basket.View;
using Example.PetShop.Basket.View.Model;
using Example.PetShop.Domain;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace Example.PetShop.Basket
{
    public class BasketModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly PetRepository _petRepository;
        private readonly AccessoryRepository _accessoryRepository;
        private readonly IRegionManager _regionManager;

        public BasketModule(IRegionManager regionManager, IUnityContainer container, PetRepository petRepository, AccessoryRepository accessoryRepository)
        {
            _regionManager = regionManager;
            _container = container;
            _petRepository = petRepository;
            _accessoryRepository = accessoryRepository;
        }

        #region IModule Members

        public void Initialize()
        {
            var basketViewModel = new BasketViewModel(_petRepository, _accessoryRepository);
            var panel = new BasketPanel(basketViewModel);
            _regionManager.Regions["Sales"].Add(panel);
            _regionManager.Regions["Sales"].Activate(panel);
        }

        #endregion
    }
}