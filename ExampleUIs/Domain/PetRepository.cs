using System;

namespace ExampleUIs.Domain
{
    public class PetRepository
    {
        private readonly History _history;

        public PetRepository(History history)
        {
            _history = history;
        }

        public History History
        {
            get { return _history; }
        }

        public void Save(Pet pet)
        {
            _history.AddText(string.Format("{0} the {1} registered at a price of £{2}", pet.Name, pet.Type, pet.Price));
        }
    }
}