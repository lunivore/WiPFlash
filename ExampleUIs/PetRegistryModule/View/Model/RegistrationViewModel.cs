#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ExampleUIs.Domain;
using ExampleUIs.PetModule.Domain;
using ExampleUIs.Utils;

#endregion

namespace ExampleUIs.PetRegistryModule.View.Model
{
    public class RegistrationViewModel
    {   
        private PetRepository _petRepository;
        private Pet _pet;

        public RegistrationViewModel(PetRepository petRepository)
        {
            _petRepository = petRepository;
            _pet = new Pet();
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

        public List<Rule> AllRules
        {
            get { return Rule.ALL;  }
        }

        public string Name
        {
            get { return _pet.Name; }
            set { _pet.Name = value; }
        }

        public string Price
        {
            get { return _pet.Price; }
            set { _pet.Price = value; }
        }

        public PetType PetType
        {
            get { return _pet.Type; } 
            set { _pet.Type = value; }
        }

        public PetFood FoodType
        {
            get { return _pet.FoodType; }
            set { _pet.FoodType = value; }
        }

        public ObservableCollection<Rule> Rules
        {
            get { return new ObservableCollection<Rule>(_pet.Rules); }
            set
            {
                _pet.Rules.Clear();
                foreach (var rule in value)
                {
                    _pet.Rules.Add(rule);
                }
            }
        }

        public ICommand SaveCommand
        {
            get { return new SavePetCommand(_petRepository, _pet);  }
        }

        public class SavePetCommand : DelegateCommand
        {
            public SavePetCommand(PetRepository repository, Pet pet)
                : base(o => repository.Save(pet))
            {
            }
        }
    }
}