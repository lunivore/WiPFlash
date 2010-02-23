#region

using System.Windows;
using ExampleUIs.PetModule.Domain;
using Microsoft.Practices.Unity;

#endregion

namespace ExampleUIs
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
