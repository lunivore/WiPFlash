#region

using System.Windows.Documents;
using ExampleUIs.Domain;

#endregion

namespace ExampleUIs.HistoryModule.View.Model
{
    public class HistoryViewModel
    {
        private readonly PetRepository _repository;
        private History _history;

        public HistoryViewModel(PetRepository repository)
        {
            _repository = repository;
            _history = repository.History;
        }

        public FlowDocument HistorySoFar
        {
            get
            {
                var doc = new FlowDocument();
                var container = new InlineUIContainer();
                doc.Blocks.Add(new Paragraph(container));

                // TODO work out how to get the history in here                

                return doc;
            }
        }
    }
}