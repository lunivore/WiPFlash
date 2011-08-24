#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Example.PetShop.Controls;
using Example.PetShop.Domain;
using Example.PetShop.Utils;
using Microsoft.Practices.Composite.Events;
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
        private PaymentMethod _paymentMethod = PaymentMethod.NoPaymentMethodSelected;

        public BasketViewModel(
            ILookAfterPets petRepository, 
            ILookAfterAccessories accessoryRepository,
            IHandleMessages messenger,
            IEventAggregator events)
        {
            _petRepository = petRepository;
            _accessoryRepository = accessoryRepository;
            _messenger = messenger;
            _petBasket = new List<Pet>();
            _accessoryBasket = new List<Accessory>();
            events.GetEvent<NewPetEvent>().Subscribe(pet => NotifyPropertyChanged("AllAvailablePets"));
            events.GetEvent<SoldPetEvent>().Subscribe(pet => NotifyPropertyChanged("AllAvailablePets"));
            _accessoryRepository.OnAccessorySelected((o, e) =>
                                                            {
                                                                foreach (var accessory in e.Accessories)
                                                                {
                                                                    if (!_accessoryBasket.Contains(accessory))
                                                                    {
                                                                        _accessoryBasket.Add(accessory);
                                                                    }
                                                                }
                                                                NotifyPropertyChanged("Basket", "HasItemsInBasket", "Total", "PurchaseAllowed");
                                                            });
            _accessoryRepository.OnAccessoryUnselected((o, e) =>
                                                              {
                                                                  _accessoryBasket.RemoveAll(
                                                                      a => e.Accessories.Contains(a));
                                                                  NotifyPropertyChanged("Basket", "HasItemsInBasket", "Total", "PurchaseAllowed");
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
                            _accessoryBasket.Clear();
                            NotifyPropertyChanged("Basket", "HasItemsInBasket", "Total");
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
                                _accessoryBasket.Clear();
                                NotifyPropertyChanged("Basket", "Total");
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
                foreach (var accessory in _accessoryBasket)
                {
                    total += accessory.PriceInPence;
                }
                return (total/100.00).ToString("0.00");
            }
        }

        public bool Cash
        {
            set { SetPaymentMethod(PaymentMethod.Cash); }
            get { return _paymentMethod == PaymentMethod.Cash; }
        }

        public bool Cheque
        {
            set { SetPaymentMethod(PaymentMethod.Cheque); }
            get { return _paymentMethod == PaymentMethod.Cheque; }
        }

        public bool Card
        {
            set { SetPaymentMethod(PaymentMethod.Card); }
            get { return _paymentMethod == PaymentMethod.Card; }
        }

        private void SetPaymentMethod(PaymentMethod method)
        {
            _paymentMethod = method;
            NotifyPropertyChanged("Cash", "Cheque", "Card", "PurchaseAllowed");
        }

        public bool VatReceipt { set; get; }

        public bool PurchaseAllowed
        {
            get
            {
                var paymentSelected = _paymentMethod.IsValid;
                return paymentSelected && HasItemsInBasket;
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