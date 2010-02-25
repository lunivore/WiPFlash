using System;
using System.Collections.Generic;
using ExampleUIs.PetModule.Domain;

namespace ExampleUIs.Domain
{
    public class Pet
    {
        public string Name
        {
            get; set;
        }

        public string Price
        {
            get; set;
        }

        public List<Rule> Rules
        {
            get; set;
        }

        public PetType Type
        {
            get; set;
        }
    }
}