#region

using System.Windows.Controls;
using Example.PetShop.AccessoryRegistry.View.Model;

#endregion

namespace Example.PetShop.AccessoryRegistry.View
{
    /// <summary>
    /// Interaction logic for AccessoryRegistryView.xaml
    /// </summary>
    public partial class AccessoryRegistryPanel : UserControl
    {
        public AccessoryRegistryPanel(AccessoryViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void accessoriesOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
