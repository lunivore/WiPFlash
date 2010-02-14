using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleUIs.Domain
{
    public class PetFood
    {
        public static readonly PetFood[] ALL = new PetFood[]
            {
                new PetFood("Herbivorous"), 
                new PetFood("Carnivorous"), 
                new PetFood("Omnivorous"),
                new PetFood("Insectivorous"),
                new PetFood("Lunivorous"),                
                new PetFood("Eats People"), 
            };

        private readonly string _foodType;

        private PetFood(string foodType)
        {
            _foodType = foodType;            
        }

        public string FoodType
        {
            get { return _foodType; }
        }

        /**
         * This string is here because the item appears in a non-editable text box, bound by the FoodType.
         * Overriding the text is the only way to access it without indexes.
         */
        public override string ToString()
        {
            return FoodType;
        }
    }
}
