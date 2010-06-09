#region

using System.Windows.Controls;
using Example.PetShop.Basket.View.Model;

#endregion

namespace Example.PetShop.Basket.View
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