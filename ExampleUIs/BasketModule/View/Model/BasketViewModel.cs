using System;
using System.Collections.Generic;
using System.ComponentModel;
using ExampleUIs.Domain;

namespace ExampleUIs.BasketModule.View.Model
{
    public class BasketViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private readonly PetRepository _repository;
        private static List<Pet> _basket;

        public BasketViewModel(PetRepository repository)
        {
            _repository = repository;
            _basket = new List<Pet>();
            _repository.PropertyChanged += (o, e) =>
                                               {
                                                   System.Console.WriteLine("New pet! Ooh, exciting.");
                                                   PropertyChanged(this, new PropertyChangedEventArgs("AllGoods"));
                                               };
        }

        public Pet[] AllGoods
        {
            get
            {
                var pets = new List<Pet>(_repository.Pets);
                foreach (var pet in _basket)
                {
                    pets.Remove(pet);
                }
                return pets.ToArray();
            }
        }

        public Pet Purchase
        {
            get
            {
                return null;
            }
            set
            {
                _basket.Add(value);
                PropertyChanged(this, new PropertyChangedEventArgs("AllGoods"));
                PropertyChanged(this, new PropertyChangedEventArgs("Purchase"));
                PropertyChanged(this, new PropertyChangedEventArgs("Basket"));
                PropertyChanged(this, new PropertyChangedEventArgs("Total"));
            }           
        }

        public string Total
        {
            get
            {
                int total = 0;
                foreach(Pet pet in _basket)
                {
                    total += pet.PriceInPence; 
                }
                return (total/100).ToString("0.00");
            }
        }
    }
}