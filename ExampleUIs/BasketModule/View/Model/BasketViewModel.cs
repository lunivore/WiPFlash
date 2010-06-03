#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ExampleUIs.Domain;
using ExampleUIs.Utils;

#endregion

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

        public string ViewHeader { get { return "Basket"; } }

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
                if (value != null && !_basket.Contains(value)){
                    _basket.Add(value);
                    PropertyChanged(this, new PropertyChangedEventArgs("Purchase"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Basket"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Total"));
                }
            }           
        }

        public BasketItem[] Basket
        {
            get
            {
                var basketContents = new List<BasketItem>();
                foreach (var pet in _basket)
                {
                    basketContents.Add(new BasketItem(pet.Name, pet.Price));
                }
                return basketContents.ToArray();
            }
        }

        public ICommand Pay
        {
            get { return new DelegateCommand(
                delegate
                {
                    foreach (var pet in _basket)
                    {
                        _repository.PetWasPutInBasket(pet);    
                    }
                    _basket.Clear();
                    PropertyChanged(this, new PropertyChangedEventArgs("Basket"));
                }); }
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
                return (total/100.00).ToString("0.00");
            }
        }
    }

    public class BasketItem
    {
        private readonly string _itemName;
        private readonly string _price;

        public BasketItem(string itemName, string price)
        {
            _itemName = itemName;
            _price = price;
        }

        public string Price
        {
            get { return _price; }
        }

        public string Item
        {
            get { return _itemName; }
        }
    }
}