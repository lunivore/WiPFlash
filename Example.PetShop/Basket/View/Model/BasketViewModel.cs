#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Example.PetShop.Domain;
using Example.PetShop.Utils;

#endregion

namespace Example.PetShop.Basket.View.Model
{
    public class BasketViewModel : INotifyPropertyChanged
    {
        private static List<Pet> _petBasket;
        private readonly PetRepository _petRepository;
        private readonly AccessoryRepository _accessoryRepository;
        private readonly List<Accessory> _accessoryBasket;

        public BasketViewModel(PetRepository petRepository, AccessoryRepository accessoryRepository)
        {
            _petRepository = petRepository;
            _accessoryRepository = accessoryRepository;
            _petBasket = new List<Pet>();
            _accessoryBasket = new List<Accessory>();
            _petRepository.UnsoldPets.CollectionChanged += (o, e) =>
                                               {
                                                   Console.WriteLine("New pet! Ooh, exciting.");
                                                   PropertyChanged(this, new PropertyChangedEventArgs("AllAvailablePets"));
                                               };
            _accessoryRepository.AccessoriesSelected += (o, e) =>
                                                            {
                                                                foreach (var accessory in e.Accessories)
                                                                {
                                                                    if (!_accessoryBasket.Contains(accessory))
                                                                    {
                                                                        _accessoryBasket.Add(accessory);
                                                                    }
                                                                }
                                                                PropertyChanged(this, new PropertyChangedEventArgs("Basket"));
                                                            };
            _accessoryRepository.AccessoriesUnselected += (o, e) =>
                                                              {
                                                                  _accessoryBasket.RemoveAll(
                                                                      a => e.Accessories.Contains(a));
                                                                  PropertyChanged(this, new PropertyChangedEventArgs("Basket"));
                                                              };
        }

        public string ViewHeader
        {
            get { return "Basket"; }
        }

        public Pet[] AllAvailablePets
        {
            get
            {
                var pets = new List<Pet>(_petRepository.UnsoldPets);
                foreach (Pet pet in _petBasket)
                {
                    pets.Remove(pet);
                }
                return pets.ToArray();
            }
        }

        public Pet Purchase
        {
            get { return null; }
            set
            {
                if (value != null && !_petBasket.Contains(value))
                {
                    _petBasket.Add(value);
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
                foreach (Pet pet in _petBasket)
                {
                    basketContents.Add(new BasketItem(pet.Name, pet.Price));
                }
                foreach (Accessory accessory in _accessoryBasket)
                {
                    basketContents.Add(new BasketItem(accessory.Name, accessory.Price));
                }
                return basketContents.ToArray();
            }
        }

        public ICommand Pay
        {
            get
            {
                return new DelegateCommand(
                    delegate
                        {
                            foreach (Pet pet in _petBasket)
                            {
                                _petRepository.PetWasPutInBasket(pet);
                            }
                            _petBasket.Clear();
                            PropertyChanged(this, new PropertyChangedEventArgs("Basket"));
                        });
            }
        }

        public string Total
        {
            get
            {
                int total = 0;
                foreach (Pet pet in _petBasket)
                {
                    total += pet.PriceInPence;
                }
                return (total/100.00).ToString("0.00");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
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