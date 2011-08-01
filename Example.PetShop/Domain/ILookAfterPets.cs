using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Example.PetShop.Domain
{
    public interface ILookAfterPets : INotifyPropertyChanged
    {
        ObservableCollection<Pet> UnsoldPets { get; }
        void Save(Pet pet);
        void PetWasSold(Pet pet);
        List<Pet> LastPets(int number);
    }
}