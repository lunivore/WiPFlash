#region

using ExampleUIs.Domain;
using ExampleUIs.PetRegistryModule.View;
using ExampleUIs.PetRegistryModule.View.Model;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace ExampleUIs.PetRegistryModule
{
    public class PetRegistryModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public PetRegistryModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            var petRepository = _container.Resolve<PetRepository>();
            var registrationViewModel = new RegistrationViewModel(petRepository);
            var view = new RegistrationPanel(registrationViewModel);
            _regionManager.Regions["Admin"].Add(view);
            _regionManager.Regions["Admin"].Activate(view);
        }
    }
}