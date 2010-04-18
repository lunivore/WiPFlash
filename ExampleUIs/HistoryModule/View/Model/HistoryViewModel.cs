#region

using System.ComponentModel;
using ExampleUIs.Domain;

#endregion

namespace ExampleUIs.HistoryModule.View.Model
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly PetRepository _repository;
        private History _history;

        public HistoryViewModel(PetRepository repository)
        {
            _repository = repository;
            _history = repository.History;
            _history.PropertyChanged +=
                delegate { PropertyChanged(this, new PropertyChangedEventArgs("HistorySoFar")); };
        }

        public string ViewHeader { get { return "History"; } }

        public string HistorySoFar
        {
            get { return _history.Text; }
        }

    }
}