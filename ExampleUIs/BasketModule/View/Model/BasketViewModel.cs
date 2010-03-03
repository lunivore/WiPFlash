using System;
using ExampleUIs.Domain;

namespace ExampleUIs.BasketModule.View.Model
{
    public class BasketViewModel
    {
        private readonly PetRepository _repository;

        public BasketViewModel(PetRepository repository)
        {
            _repository = repository;
        }

        public Purchaseable[] AllGoods
        {
            get { return new Purchaseable[0]; }
        }


        public class Purchaseable
        {
        }
    }
}