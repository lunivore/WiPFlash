#region

using System.Collections.Generic;

#endregion

namespace Example.PetShop.Domain
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

        private readonly string _text;

        private PetFood(string Text)
        {
            _text = Text;
        }

        public string Text
        {
            get { return _text; }
        }

        /**
         * This string is here because the item appears in a non-editable text box, bound by the FoodType.
         * Overriding the text is the only way to access it without indexes.
         */

        public override string ToString()
        {
            return "PetFood[" + Text + "]";
        }
    }
}