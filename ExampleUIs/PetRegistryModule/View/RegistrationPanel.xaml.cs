#region

using ExampleUIs.PetRegistryModule.View.Model;

#endregion

namespace ExampleUIs.PetRegistryModule.View
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