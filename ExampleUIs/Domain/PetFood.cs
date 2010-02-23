#region

using System.Collections.Generic;

#endregion

namespace ExampleUIs.PetModule.Domain
{
    public class PetFood
    {
        public static readonly List<PetFood> ALL = new List<PetFood>(
            new[]
             {
                 new PetFood("Herbivorous"), 
                 new PetFood("Carnivorous"), 
                 new PetFood("Omnivorous"),
                 new PetFood("Insectivorous"),
                 new PetFood("Lunivorous"),                
                 new PetFood("Eats People"), 
             });

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
            return "PetFood[" + FoodType + "]";
        }
    }
}