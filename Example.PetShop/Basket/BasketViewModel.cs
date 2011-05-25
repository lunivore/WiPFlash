#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Example.PetShop.Controls;
using Example.PetShop.Domain;
using Example.PetShop.Utils;
using Microsoft.Practices.Composite.Presentation.Commands;

#endregion

namespace Example.PetShop.Basket
{
    public class BasketViewModel : INotifyPropertyChanged, IHaveATitle
    {
        private static List<Pet> _petBasket;
        private readonly ILookAfterPets _petRepository;
        private readonly ILookAfterAccessories _accessoryRepository;
        private readonly IHandleMessages _messenger;
        private readonly List<Accessory> _accessoryBasket;

        public BasketViewModel(
            ILookAfterPets petRepository, 
            ILookAfterAccessories accessoryRepository,
            IHandleMessages messenger)
        {
            _petRepository = petRepository;
            _accessoryRepository = accessoryRepository;
            _messenger = messenger;
            _petBasket = new List<Pet>();
            _accessoryBasket = new List<Accessory>();
            _petRepository.UnsoldPets.CollectionChanged += (o, e) => NotifyPropertyChanged("AllAvailablePets");
            _accessoryRepository.OnAccessorySelected((o, e) =>
                                                            {
                                                                foreach (var accessory in e.Accessories)
                                                                {
                                                                    if (!_accessoryBasket.Contains(accessory))
                                                                    {
                                                                        _accessoryBasket.Add(accessory);
                                                                    }
                                                                }
                                                                NotifyPropertyChanged("Basket", "HasItemsInBasket");
                                                            });
            _accessoryRepository.OnAccessoryUnselected((o, e) =>
                                                              {
                                                                  _accessoryBasket.RemoveAll(
                                                                      a => e.Accessories.Contains(a));
                                                                  NotifyPropertyChanged("Basket", "HasItemsInBasket");
                                                              });                                  
        }

        private void NotifyPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Title
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

        public Pet PetSelectedForPurchase
        {
            get { return null; }
            set
            {
                if (value != null && !_petBasket.Contains(value))
                {
                    _petBasket.Add(value);
                    NotifyPropertyChanged("AllAvailablePets", "PetSelectedForPurchase", "Basket", "Total", "HasItemsInBasket");
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

        public bool HasItemsInBasket
        {
            get { return _petBasket.Count > 0 || _accessoryBasket.Count > 0; }
        }

        public ICommand Pay
        {
            get
            {
                return new DelegateCommand<object>(
                    delegate
                        {
                            foreach (Pet pet in _petBasket)
                            {
                                _petRepository.PetWasSold(pet);
                            }
                            _petBasket.Clear();
                            NotifyPropertyChanged("Basket", "HasItemsInBasket");
                        });
            }
        }

        public ICommand Reset
        {
            get
            {
                return new DelegateCommand<object>(
                    delegate
                        {
                            if (_messenger.Show("Please confirm", "Are you sure you want to clear the contents of the basket?"))
                            {
                                _petBasket.Clear();    
                            }                            
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

        public bool Cash{ set; get; }
        public bool Cheque { set; get; }
        public bool Card { set; get; }
        public bool VatReceipt { set; get; }

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