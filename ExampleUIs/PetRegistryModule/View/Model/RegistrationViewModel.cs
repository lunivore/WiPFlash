#region

using System.Collections.Generic;
using ExampleUIs.PetModule.Domain;

#endregion

namespace ExampleUIs.PetRegistryModule.View.Model
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

        public List<Rule> Rules
        {
            get { return Rule.ALL;  }
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