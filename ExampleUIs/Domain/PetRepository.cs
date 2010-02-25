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
            string petType = (pet.Type == null) ? string.Empty : pet.Type.Name;
            string petFood = (pet.FoodType == null) ? string.Empty : pet.FoodType.Text;
            _history.AddText(string.Format("{0} the {1} registered at a price of £{2}. Food: {3}", pet.Name, petType, pet.Price, petFood));
        }
    }
}