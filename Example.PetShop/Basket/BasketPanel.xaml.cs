#region

using System.Windows.Controls;

#endregion

namespace Example.PetShop.Basket
{
    /// <summary>
    /// Interaction logic for BasketPanel.xaml
    /// </summary>
    public partial class BasketPanel : StackPanel
    {
        public BasketPanel(BasketViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}