#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using Example.PetShop.Domain;
using Example.PetShop.Utils;
using Microsoft.Practices.Composite.Events;

#endregion

namespace Example.PetShop.History
{
    public class HistoryViewModel : INotifyPropertyChanged, IHaveATitle
    {
        private readonly Domain.History _history;
        private readonly PetRepository _repository;

        public HistoryViewModel(PetRepository repository, IEventAggregator events)
        {
            _repository = repository;
            _history = repository.History;
            events.GetEvent<NewPetEvent>().Subscribe(pet => PetsChanged());
            events.GetEvent<SoldPetEvent>().Subscribe(pet => PetsChanged());
        }

        private void PetsChanged()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("HistorySoFar"));
            PropertyChanged(this, new PropertyChangedEventArgs("LastThreePets"));
        }

        public string Title
        {
            get { return "History"; }
        }

        public ObservableCollection<Pet> LastThreePets
        {
            get { return new ObservableCollection<Pet>(_repository.LastPets(3)); }
        }

        public string HistorySoFar
        {
            get { return _history.Text; }
            set { _history.Text = value; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }
}