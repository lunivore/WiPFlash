#region

using Example.PetShop.Domain;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace Example.PetShop.PetRegistry
{
    public class PetRegistryModule : IModule
    {
        private readonly PetRepository _petRepository;
        private readonly IRegionManager _regionManager;

        public PetRegistryModule(IRegionManager regionManager, PetRepository petRepository)
        {
            _regionManager = regionManager;
            _petRepository = petRepository;
        }

        #region IModule Members

        public void Initialize()
        {
            var registrationViewModel = new RegistrationViewModel(_petRepository);
            var view = new RegistrationPanel(registrationViewModel);
            _regionManager.Regions["Admin"].Add(view);
            _regionManager.Regions["Admin"].Activate(view);
        }

        #endregion
    }
}