#region

using System.Windows.Controls;
using ExampleUIs.BasketModule.View.Model;

#endregion

namespace ExampleUIs.BasketModule.View
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
