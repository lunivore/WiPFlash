#region

using ExampleUIs.Domain;
using ExampleUIs.HistoryModule.View;
using ExampleUIs.HistoryModule.View.Model;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace ExampleUIs.HistoryModule
{
    public class HistoryModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public HistoryModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            var petRepository = _container.Resolve<PetRepository>();
            var historyViewModel = new HistoryViewModel(petRepository);
            _regionManager.Regions["Admin"].Add(new HistoryPanel(historyViewModel));
        }
    }
}