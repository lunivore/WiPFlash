#region



#endregion

using System;

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

        private void copyPetContextTarget_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.GetPosition(copyPetContextTarget);
            Console.WriteLine("Click happened at {0},{1}", position.X, position.Y);

            var bounds = copyPetContextTarget.PointToScreen(e.GetPosition(copyPetContextTarget));
            Console.WriteLine("In screen coords: {0}, {1}", bounds.X, bounds.Y);
        }
    }
}