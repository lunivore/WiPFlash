#region

using Example.PetShop.Domain;
using Example.PetShop.PetRegistry.View;
using Example.PetShop.PetRegistry.View.Model;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace Example.PetShop.PetRegistry
{
    public class PetRegistryModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public PetRegistryModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            var petRepository = _container.Resolve<PetRepository>();
            var registrationViewModel = new RegistrationViewModel(petRepository);
            var view = new RegistrationPanel(registrationViewModel);
            _regionManager.Regions["Admin"].Add(view);
            _regionManager.Regions["Admin"].Activate(view);
        }

        #endregion
    }
}