#region

using System.Collections.ObjectModel;
using Example.PetShop.Domain;

#endregion

namespace Example.PetShop.AccessoryRegistry.View.Model
{
    public class AccessoryViewModel
    {
        private readonly AccessoryRepository _repository;

        public AccessoryViewModel(AccessoryRepository repository)
        {
            _repository = repository;
        }

        public string ViewHeader
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