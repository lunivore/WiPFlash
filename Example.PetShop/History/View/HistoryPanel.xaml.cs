#region

using Example.PetShop.History.View.Model;

#endregion

namespace Example.PetShop.History.View
{
    public partial class HistoryPanel
    {
        public HistoryPanel(HistoryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}