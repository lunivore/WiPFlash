#region

using ExampleUIs.PetModule.View.Model;

#endregion

namespace ExampleUIs.PetModule.View
{
    public partial class RegistrationPanel
    {
        public RegistrationPanel(RegistrationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}