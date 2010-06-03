#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using ExampleUIs.PetModule.Domain;

#endregion

namespace ExampleUIs.Domain
{
    public class Pet : INotifyPropertyChanged
    {
        private bool _sold;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
     
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

        public bool Sold
        {
            get { return _sold; }
            set {
                _sold = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Sold"));
            }
        }

        public override string ToString()
        {
            return "Pet[" + Name + "]";
        }

    }
}