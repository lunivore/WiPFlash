#region

using System;
using ExampleUIs.PetRegistryModule.View.Model;

#endregion

namespace ExampleUIs.PetRegistryModule.View
{
    public partial class RegistrationPanel
    {
        private RegistrationViewModel _viewModel;

        public RegistrationPanel(RegistrationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
        }

        protected RegistrationViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }
}