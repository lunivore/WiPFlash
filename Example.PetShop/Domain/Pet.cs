#region

using System;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace Example.PetShop.Domain
{
    public class Pet : INotifyPropertyChanged
    {
        private bool _sold;

        public string Name { get; set; }

        public int PriceInPence
        { 
            get;
            set;
        }

        public string Price
        {
            get { return (PriceInPence / 100.00).ToString("0.00"); }
        }

        public List<Rule> Rules { get; set; }

        public PetType Type { get; set; }

        public PetFood FoodType { get; set; }

        public bool Sold
        {
            get { return _sold; }
            set
            {
                _sold = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Sold"));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        public override string ToString()
        {
            return "Pet[" + Name + "]";
        }

        public Pet CopyDetailsWithName(string name)
        {
            return new Pet
                       {
                           Name = name,
                           _sold = false,
                           FoodType = FoodType,
                           PriceInPence = PriceInPence,
                           Rules = Rules,
                           Type = Type
                       };
        }
    }
}