#region

using System.Collections.Generic;

#endregion

namespace Example.PetShop.Domain
{
    public class PetType
    {
        public static List<PetType> ALL = new List<PetType>
                                              {
                                                  new PetType("Cat"),
                                                  new PetType("Dog"),
                                                  new PetType("Rabbit"),
                                                  new PetType("Rodent"),
                                                  new PetType("Large Bird"),
                                                  new PetType("Small Bird"),
                                                  new PetType("Reptile"),
                                                  new PetType("Fish")
                                              };

        private readonly string _typeName;

        public PetType(string typeName)
        {
            _typeName = typeName;
        }

        public string Name
        {
            get { return _typeName; }
        }

        public override string ToString()
        {
            return "PetType[" + Name + "]";
        }
    }
}