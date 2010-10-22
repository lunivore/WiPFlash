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
        private readonly PetRepository _petRepository;
        private readonly IRegionManager _regionManager;

        public HistoryModule(IRegionManager regionManager, PetRepository petRepository)
        {
            _regionManager = regionManager;
            _petRepository = petRepository;
        }

        #region IModule Members

        public void Initialize()
        {
            var historyViewModel = new HistoryViewModel(_petRepository);
            _regionManager.Regions["Admin"].Add(new HistoryPanel(historyViewModel));
        }

        #endregion
    }
}