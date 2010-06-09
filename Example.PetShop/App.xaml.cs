#region

using System.Windows;
using Example.PetShop;

#endregion

namespace Example.PetShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new BootStrapper();
            bootstrapper.Run();
        }
    }
}