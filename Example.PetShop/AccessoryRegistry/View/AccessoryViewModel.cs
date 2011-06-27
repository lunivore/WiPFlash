#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Example.PetShop.Domain;
using Example.PetShop.Utils;

#endregion

namespace Example.PetShop.AccessoryRegistry.View
{
    public class AccessoryViewModel : IHaveATitle
    {
        private readonly AccessoryRepository _repository;

        public AccessoryViewModel(AccessoryRepository repository)
        {
            _repository = repository;
        }

        public string Title
        {
            get { return "Accessories"; }
        }

        public ObservableCollection<Accessory> Accessories
        {
            get { return _repository.Accessories; }
        }

        public ObservableCollection<Accessory> SelectedAccessories
        {
            get { return _repository.SelectedAccessories; }
        }
    }
}