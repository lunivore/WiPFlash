#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ExampleUIs.Domain;
using ExampleUIs.PetModule.Domain;
using ExampleUIs.Utils;

#endregion

namespace ExampleUIs.PetRegistryModule.View.Model
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    
        private readonly PetRepository _petRepository;
        private Pet _pet;

        public RegistrationViewModel(PetRepository petRepository)
        {
            _petRepository = petRepository;
            _pet = new Pet();
        }

        public string ViewHeader { get { return "Registration";  } }

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
            get { return new SavePetCommand(this);  }
        }

        public class SavePetCommand : DelegateCommand
        {
            internal SavePetCommand(RegistrationViewModel model)
                : base(o=>
                    {
                        model._petRepository.Save(model._pet);
                        model._pet = new Pet();
                        model.NotifyPropertyChanged("Name", "Price", "PetType", "FoodType", "Rules");
                    })
            {
            }
        }

        private void NotifyPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}