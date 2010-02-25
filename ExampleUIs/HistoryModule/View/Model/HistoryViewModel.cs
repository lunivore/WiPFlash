#region

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
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

        public string HistorySoFar
        {
            get { return _history.Text; }
        }

    }
}