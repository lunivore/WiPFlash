#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using Example.PetShop.Domain;

#endregion

namespace Example.PetShop.History.View.Model
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        private readonly Domain.History _history;
        private readonly PetRepository _repository;

        public HistoryViewModel(PetRepository repository)
        {
            _repository = repository;
            _history = repository.History;
            _repository.PropertyChanged +=
                delegate
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("HistorySoFar"));
                        PropertyChanged(this, new PropertyChangedEventArgs("LastThreePets"));
                    };
        }

        public string ViewHeader
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
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }
}