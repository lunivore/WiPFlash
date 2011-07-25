#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Example.PetShop.Domain;
using Example.PetShop.Utils;
using Microsoft.Practices.Composite.Presentation.Commands;

#endregion

namespace Example.PetShop.PetRegistry
{
    public class RegistrationViewModel : INotifyPropertyChanged, IHaveATitle
    {
        private readonly ILookAfterPets _petRepository;
        private Pet _pet;

        public RegistrationViewModel(ILookAfterPets petRepository)
        {
            _petRepository = petRepository;
            _pet = new Pet();
        }

        public IList<Pet> Pets
        {
            get { return _petRepository.UnsoldPets; }
        }

        public string Title
        {
            get { return "Registration"; }
        }

        public List<PetType> PetTypes
        {
            get { return PetType.ALL; }
        }

        public List<PetFood> FoodTypes
        {
            get { return PetFood.ALL; }
        }

        public List<Rule> AllRules
        {
            get { return Rule.ALL; }
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
                foreach (Rule rule in value)
                {
                    _pet.Rules.Add(rule);
                }
            }
        }

        public ICommand SaveCommand
        {
            get { return new SavePetCommand(this); }
        }

        public ICommand CopyCommand
        {
            get { return new CopyPetCommand(this); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        private void NotifyPropertyChanged(params string[] properties)
        {
            foreach (string property in properties)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #region Nested type: SavePetCommand

        public class SavePetCommand : DelegateCommand<object>
        {
            internal SavePetCommand(RegistrationViewModel model)
                : base(o =>
                           {
                               model._petRepository.Save(model._pet);
                               model._pet = new Pet();
                               model.NotifyPropertyChanged("Name", "Price", "PetType", "FoodType", "Rules");
                           })
            {
            }
        }

        #endregion

        #region Nested type: CopyPetCommand

        public class CopyPetCommand : DelegateCommand<object>
        {
            internal CopyPetCommand(RegistrationViewModel model)
                : base(o =>
                {
                    var pet = (Pet) o;
                    model._pet = pet.CopyDetailsWithName(model._pet.Name);
                    model.NotifyPropertyChanged("Name", "Price", "PetType", "FoodType", "Rules");
                })
            {
            }
        }

        #endregion       
    }
}