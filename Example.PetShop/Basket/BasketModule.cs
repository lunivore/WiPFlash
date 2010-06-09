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
        private readonly IRegionManager _regionManager;

        public BasketModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            var petRepository = _container.Resolve<PetRepository>();
            var basketViewModel = new BasketViewModel(petRepository);
            var panel = new BasketPanel(basketViewModel);
            _regionManager.Regions["Sales"].Add(panel);
            _regionManager.Regions["Sales"].Activate(panel);
        }

        #endregion
    }
}