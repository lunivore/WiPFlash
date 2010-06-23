#region

using System.Windows;

#endregion

namespace Example.PetShop
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(ShellViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}