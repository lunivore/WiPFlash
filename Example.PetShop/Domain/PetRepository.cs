#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using Microsoft.Practices.Composite.Events;

#endregion

namespace Example.PetShop.Domain
{
    public class PetRepository : ILookAfterPets
    {
        private readonly History _history;
        private readonly IEventAggregator _events;
        private readonly List<Pet> _unsoldPets;
        private readonly List<Pet> _pets;

        public PetRepository(History history, IEventAggregator events)
        {
            _history = history;
            _events = events;
            _pets = new List<Pet>
                        {
                            new Pet
                                {
                                    Name = "Spot",
                                    Type = PetType.ALL[2],
                                    FoodType = PetFood.ALL[1],
                                    PriceInPence = 10000
                                },
                            new Pet
                                {
                                    Name = "Cinnamon",
                                    Type = PetType.ALL[1],
                                    FoodType = PetFood.ALL[0],
                                    PriceInPence = 450
                                },
                            new Pet
                                {
                                    Name = "Dancer",
                                    Type = PetType.ALL[2],
                                    FoodType = PetFood.ALL[2],
                                    PriceInPence = 5400
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

        public void Save(Pet pet)
        {
            string petType = (pet.Type == null) ? string.Empty : pet.Type.Name;
            string petFood = (pet.FoodType == null) ? string.Empty : pet.FoodType.Text;
            _history.AddText(string.Format("{0} the {1} registered at a price of £{2}. Food: {3}", pet.Name, petType,
                                           pet.Price, petFood));

            // Mimics adding to a real database
            new Thread(() =>
                           {
                               Thread.Sleep(400);
                               _pets.Add(pet);
                               _unsoldPets.Add(pet);
                               _events.GetEvent<NewPetEvent>().Publish(pet);
                           }).Start();
        }

        public void PetWasSold(Pet pet)
        {
            // Mimics adding to a real database
            new Thread(() =>
            {
                Thread.Sleep(400);
                pet.Sold = true;
                _unsoldPets.Remove(pet);
                _events.GetEvent<SoldPetEvent>().Publish(pet);
            }).Start();
        }

        public List<Pet> LastPets(int number)
        {
            return new List<Pet>(_pets).GetRange(_pets.Count - (number), number);
        }
    }
}