#region

using System.Collections.Generic;
using ExampleUIs.PetModule.Domain;

#endregion

namespace ExampleUIs.Domain
{
    public class Pet
    {
        public string Name
        {
            get;
            set;
        }

        public string Price
        {
            get;
            set;
        }

        public int PriceInPence
        {
            get
            {
                return int.Parse((double.Parse(Price)*100.00).ToString());
            }
        }

        public List<Rule> Rules
        {
            get;
            set;
        }

        public PetType Type
        {
            get;
            set;
        }

        public PetFood FoodType
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "Pet[" + Name + "]";
        }
    }
}