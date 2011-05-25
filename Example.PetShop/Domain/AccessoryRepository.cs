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
                               new Accessory {Name = "Dog Collar (Large)", Price = "10.00"},
                               new Accessory {Name = "Dog Collar (Small)", Price = "9.00"},
                               new Accessory {Name = "Cat Collar", Price = "5.00"},
                               new Accessory {Name = "Collar Tag", Price = "1.00"},
                               new Accessory {Name = "Paws'n'claws Tin", Price = "0.90"},
                               new Accessory {Name = "Bark'n'bite Packet", Price = "0.80"},
                               new Accessory {Name = "Straw (Large bag)", Price = "5.00"},
                               new Accessory {Name = "Straw (Small bag)", Price = "3.00"},
                               new Accessory {Name = "Sawdust (Large bag)", Price = "5.00"},
                               new Accessory {Name = "Sawdust (Small bag)", Price = "3.00"},
                               new Accessory {Name = "Rabbit hutch", Price = "100.00"},
                               new Accessory {Name = "Palace hamster cage", Price = "80.00"},
                               new Accessory {Name = "Wire hamster cage", Price = "10.00"},
                               new Accessory {Name = "Netting (per m)", Price = "1.00"},
                               new Accessory {Name = "Bird cage (Large)", Price = "50.00"},
                               new Accessory {Name = "Bird cage (Small)", Price = "30.00"},
                               new Accessory {Name = "Fish tank (Large)", Price = "85.00"},
                               new Accessory {Name = "Fish tank (Medium)", Price = "75.00"},
                               new Accessory {Name = "Fish tank (Small)", Price = "60.00"},
                               new Accessory {Name = "Hamster wheel", Price = "2.00"},
                               new Accessory {Name = "Ceramic bridge", Price = "5.00"},
                               new Accessory {Name = "Ceramic skull", Price = "4.50"},
                               new Accessory {Name = "Ceramic chest with gold", Price = "5.65"},
                               new Accessory {Name = "Water plant", Price = "2.50"},
                               new Accessory {Name = "Catnip mouse", Price = "3.00"},
                               new Accessory {Name = "Jingle mouse", Price = "2.00"},
                               new Accessory {Name = "Rubber bone", Price = "1.50"},
                               new Accessory {Name = "Pigskin bone", Price = "3.00"},
                               new Accessory {Name = "Dog bowl", Price = "1.50"},
                               new Accessory {Name = "Cat bowl", Price = "1.50"},
                               new Accessory {Name = "Misc pet bowl", Price = "1.50"},
                               new Accessory {Name = "Flea powder", Price = "4.50"},
                               new Accessory {Name = "Flea collar (Large Dog)", Price = "12.00"},
                               new Accessory {Name = "Flea collar (Small Dog)", Price = "10.00"},
                               new Accessory {Name = "Flea collar (Cat)", Price = "9.00"},
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