#region



#endregion

namespace Example.PetShop.PetRegistry
{
    public partial class RegistrationPanel
    {
        private readonly RegistrationViewModel _viewModel;

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