#region

using Example.PetShop.Domain;
using Example.PetShop.History.View;
using Example.PetShop.History.View.Model;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace Example.PetShop.History
{
    public class HistoryModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public HistoryModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            var petRepository = _container.Resolve<PetRepository>();
            var historyViewModel = new HistoryViewModel(petRepository);
            _regionManager.Regions["Admin"].Add(new HistoryPanel(historyViewModel));
        }

        #endregion
    }
}