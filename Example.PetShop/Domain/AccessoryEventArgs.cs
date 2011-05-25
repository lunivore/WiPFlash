using System;
using System.Collections.Generic;

namespace Example.PetShop.Domain
{
    public class AccessoryEventArgs : EventArgs
    {
        private readonly List<Accessory> _accessories;

        public AccessoryEventArgs(List<Accessory> accessories)
        {
            _accessories = accessories;
        }

        public IList<Accessory> Accessories
        {
            get { return _accessories; }
        }
    }
}