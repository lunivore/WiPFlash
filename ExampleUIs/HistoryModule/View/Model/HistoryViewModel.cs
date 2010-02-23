using System;
using ExampleUIs.PetModule.Domain;

namespace ExampleUIs.HistoryModule.View.Model
{
    public class HistoryViewModel
    {
        private readonly PetRepository _repository;

        public HistoryViewModel(PetRepository repository)
        {
            _repository = repository;
        }
    }
}