using System;
using ExampleUIs.PetModule.Domain;
using ExampleUIs.PetModule.View;
using ExampleUIs.PetModule.View.Model;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;

namespace ExampleUIs.PetRegistryModule
{
    public class PetRegistryModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public PetRegistryModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {            
            var petRepository = new PetRepository(new History());
            var registrationViewModel = new RegistrationViewModel(petRepository);
            _regionManager.Regions["RegistryRegion"].Add(new RegistrationPanel(registrationViewModel));
        }
    }
}