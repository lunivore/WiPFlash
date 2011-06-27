#region

using Example.PetShop.AccessoryRegistry.View;
using Example.PetShop.Domain;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace Example.PetShop.AccessoryRegistry
{
    public class AccessoryRegistryModule : IModule
    {
        private readonly AccessoryRepository _accessoryRepository;
        private readonly IRegionManager _regionManager;

        public AccessoryRegistryModule(IRegionManager regionManager, AccessoryRepository accessoryRepository)
        {
            _regionManager = regionManager;
            _accessoryRepository = accessoryRepository;
        }

        #region IModule Members

        public void Initialize()
        {
            var viewModel = new AccessoryViewModel(_accessoryRepository);
            var panel = new AccessoryRegistryPanel(viewModel);
            _regionManager.Regions["Sales"].Add(panel);
        }

        #endregion
    }
}