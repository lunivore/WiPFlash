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

        public string Price
        {
            get; set;
        }

        public override string ToString()
        {
            return String.Format("Accessory[{0}]", Name);
        }
    }
}
