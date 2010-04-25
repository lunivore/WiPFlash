#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using ExampleUIs.Domain;

#endregion

namespace ExampleUIs.HistoryModule.View.Model
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly PetRepository _repository;
        private readonly History _history;

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

        public string ViewHeader { get { return "History"; } }

        public ObservableCollection<Pet> LastThreePets
        {
            get { return new ObservableCollection<Pet>(_repository.LastPets(3));  }
        }

        public string HistorySoFar
        {
            get { return _history.Text; }
        }
    }
}