#region

using System;
using System.Collections.Generic;

#endregion

namespace Example.PetShop.Domain
{
    public class Accessory
    {
        public string Name
        {
            get; set;
        }

        public int PriceInPence
        {
            get; set;
        }

        public string Price
        {
            get { return (PriceInPence/100.00).ToString("0.00"); }
        }

        public override string ToString()
        {
            return String.Format("Accessory[{0}]", Name);
        }
    }
}
