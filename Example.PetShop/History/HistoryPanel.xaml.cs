#region



#endregion

namespace Example.PetShop.History
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