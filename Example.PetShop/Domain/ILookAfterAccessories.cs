using System;
using System.Collections.ObjectModel;

namespace Example.PetShop.Domain
{
    public interface ILookAfterAccessories
    {
        ObservableCollection<Accessory> Accessories { get; }
        ObservableCollection<Accessory> SelectedAccessories { get; }
        void OnAccessorySelected(EventHandler<AccessoryEventArgs> eventHandler);
        void OnAccessoryUnselected(EventHandler<AccessoryEventArgs> eventHandler);
    }
}