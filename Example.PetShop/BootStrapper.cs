#region

using System.Windows;
using Example.PetShop.Domain;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;

#endregion

namespace Example.PetShop
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var history = new Domain.History();
            var petRepository = new PetRepository(history);
            Container.RegisterInstance(history);
            Container.RegisterInstance(petRepository);
            var shell = Container.Resolve<Shell>();
            shell.Show();
            return shell;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}