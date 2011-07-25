#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

#endregion

namespace Example.PetShop.Domain
{
    public class PetRepository : ILookAfterPets
    {
        private readonly History _history;
        private readonly List<Pet> _unsoldPets;
        private readonly List<Pet> _pets;

        public PetRepository(History history)
        {
            _history = history;
            _pets = new List<Pet>
                        {
                            new Pet
                                {
                                    Name = "Spot",
                                    Type = PetType.ALL[2],
                                    FoodType = PetFood.ALL[1],
                                    Price = "100.00"
                                },
                            new Pet
                                {
                                    Name = "Cinnamon",
                                    Type = PetType.ALL[1],
                                    FoodType = PetFood.ALL[0],
                                    Price = "4.50"
                                },
                            new Pet
                                {
                                    Name = "Dancer",
                                    Type = PetType.ALL[2],
                                    FoodType = PetFood.ALL[2],
                                    Price = "54.00"
                                },
                        };
            _unsoldPets = new List<Pet>(_pets);
        }

        public History History
        {
            get { return _history; }
        }

        public virtual IList<Pet> UnsoldPets
        {
            get
            {
                return _unsoldPets;
            }
        }

        public IList<Pet> Pets
        {
            get { return _pets; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        public void Save(Pet pet)
        {
            string petType = (pet.Type == null) ? string.Empty : pet.Type.Name;
            string petFood = (pet.FoodType == null) ? string.Empty : pet.FoodType.Text;
            _history.AddText(string.Format("{0} the {1} registered at a price of £{2}. Food: {3}", pet.Name, petType,
                                           pet.Price, petFood));

            new Thread(() =>
                           {
                               Thread.Sleep(400);
                               _pets.Add(pet);
                               _unsoldPets.Add(pet);
                               PropertyChanged(this, new PropertyChangedEventArgs("Pets"));
                           }).Start();
        }

        public void PetWasSold(Pet pet)
        {
            pet.Sold = true;
            _unsoldPets.Remove(pet);
            PropertyChanged(this, new PropertyChangedEventArgs("Pets"));
        }

        public List<Pet> LastPets(int number)
        {
            return new List<Pet>(_pets).GetRange(_pets.Count - (number), number);
        }
    }
}