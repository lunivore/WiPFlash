#region

using System.Collections.Generic;
using ExampleUIs.PetModule.Domain;
using Microsoft.Practices.Unity;

#endregion

namespace ExampleUIs.PetModule.View.Model
{
    public class RegistrationViewModel
    {
        private PetRepository _petRepository;

        public RegistrationViewModel(PetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public List<PetType> PetTypes {
            get
            {
                return PetType.ALL;
            }
        }

        public List<PetFood> FoodTypes
        {
            get
            {
                return PetFood.ALL;
            }
        }

        public string Name
        {
            get; set;
        }

        public string Price
        {
            get; set;
        }
    }
}