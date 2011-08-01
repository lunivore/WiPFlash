#region

using System;
using System.Collections.ObjectModel;
using Example.PetShop.Utils;

#endregion

namespace Example.PetShop.Domain
{
    public class AccessoryRepository : ILookAfterAccessories
    {
        private readonly ObservableCollection<Accessory> _selectedAccessories;

        private EventHandler<AccessoryEventArgs> _accessoriesSelected = delegate { };
        private EventHandler<AccessoryEventArgs> _accessoriesUnselected = delegate { };

        public AccessoryRepository()
        {
            _selectedAccessories = new ObservableCollection<Accessory>();
            _selectedAccessories.CollectionChanged += (src, e) =>
                                                          {
                                                              if (e.NewItems != null && e.NewItems.Count > 0)
                                                              {
                                                                  _accessoriesSelected(this, new AccessoryEventArgs(CollectionUtils.Convert(e.NewItems, o => (Accessory)o)));
                                                              }
                                                              if (e.OldItems != null && e.OldItems.Count > 0)
                                                              {
                                                                  _accessoriesUnselected(this, new AccessoryEventArgs(CollectionUtils.Convert(e.OldItems, o => (Accessory)o)));
                                                              }
                                                          };
        }

        public ObservableCollection<Accessory> Accessories
        {
            get
            {
                return new ObservableCollection<Accessory>
                           {
                               new Accessory {Name = "Dog Collar (Large)", PriceInPence = 1000},
                               new Accessory {Name = "Dog Collar (Small)", PriceInPence = 900},
                               new Accessory {Name = "Cat Collar", PriceInPence = 500},
                               new Accessory {Name = "Collar Tag", PriceInPence = 100},
                               new Accessory {Name = "Paws'n'claws Tin", PriceInPence = 090},
                               new Accessory {Name = "Bark'n'bite Packet", PriceInPence = 080},
                               new Accessory {Name = "Straw (Large bag)", PriceInPence = 500},
                               new Accessory {Name = "Straw (Small bag)", PriceInPence = 300},
                               new Accessory {Name = "Sawdust (Large bag)", PriceInPence = 500},
                               new Accessory {Name = "Sawdust (Small bag)", PriceInPence = 300},
                               new Accessory {Name = "Rabbit hutch", PriceInPence = 10000},
                               new Accessory {Name = "Palace hamster cage", PriceInPence = 8000},
                               new Accessory {Name = "Wire hamster cage", PriceInPence = 1000},
                               new Accessory {Name = "Netting (per m)", PriceInPence = 100},
                               new Accessory {Name = "Bird cage (Large)", PriceInPence = 5000},
                               new Accessory {Name = "Bird cage (Small)", PriceInPence = 3000},
                               new Accessory {Name = "Fish tank (Large)", PriceInPence = 8500},
                               new Accessory {Name = "Fish tank (Medium)", PriceInPence = 7500},
                               new Accessory {Name = "Fish tank (Small)", PriceInPence = 6000},
                               new Accessory {Name = "Hamster wheel", PriceInPence = 200},
                               new Accessory {Name = "Ceramic bridge", PriceInPence = 500},
                               new Accessory {Name = "Ceramic skull", PriceInPence = 450},
                               new Accessory {Name = "Ceramic chest with gold", PriceInPence = 565},
                               new Accessory {Name = "Water plant", PriceInPence = 250},
                               new Accessory {Name = "Catnip mouse", PriceInPence = 300},
                               new Accessory {Name = "Jingle mouse", PriceInPence = 200},
                               new Accessory {Name = "Rubber bone", PriceInPence = 150},
                               new Accessory {Name = "Pigskin bone", PriceInPence = 300},
                               new Accessory {Name = "Dog bowl", PriceInPence = 150},
                               new Accessory {Name = "Cat bowl", PriceInPence = 150},
                               new Accessory {Name = "Misc pet bowl", PriceInPence = 150},
                               new Accessory {Name = "Flea powder", PriceInPence = 450},
                               new Accessory {Name = "Flea collar (Large Dog)", PriceInPence = 1200},
                               new Accessory {Name = "Flea collar (Small Dog)", PriceInPence = 1000},
                               new Accessory {Name = "Flea collar (Cat)", PriceInPence = 900},
                           };
            }
        }

        public ObservableCollection<Accessory> SelectedAccessories
        {
            get { return _selectedAccessories; }
        }

        public void OnAccessorySelected(EventHandler<AccessoryEventArgs> eventHandler)
        {
            _accessoriesSelected += eventHandler;
        }

        public void OnAccessoryUnselected(EventHandler<AccessoryEventArgs> eventHandler)
        {
            _accessoriesUnselected += eventHandler;
        }
    }
}