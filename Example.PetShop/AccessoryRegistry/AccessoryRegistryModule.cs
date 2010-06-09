#region

using Example.PetShop.AccessoryRegistry.View;
using Example.PetShop.AccessoryRegistry.View.Model;
using Example.PetShop.Domain;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace Example.PetShop.AccessoryRegistry
{
    public class AccessoryRegistryModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public AccessoryRegistryModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            var repository = _container.Resolve<AccessoryRepository>();
            var viewModel = new AccessoryViewModel(repository);
            var panel = new AccessoryRegistryPanel(viewModel);
            _regionManager.Regions["Sales"].Add(panel);
        }

        #endregion
    }
}