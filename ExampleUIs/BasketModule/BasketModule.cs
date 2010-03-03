#region

using System;
using ExampleUIs.BasketModule.View;
using ExampleUIs.BasketModule.View.Model;
using ExampleUIs.Domain;
using ExampleUIs.PetRegistryModule.View;
using ExampleUIs.PetRegistryModule.View.Model;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace ExampleUIs.BasketModule
{
    public class BasketModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public BasketModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            var petRepository = _container.Resolve<PetRepository>();
            var basketViewModel = new BasketViewModel(petRepository);
            _regionManager.Regions["BasketRegion"].Add(new BasketPanel(basketViewModel));
        }
    }    
}