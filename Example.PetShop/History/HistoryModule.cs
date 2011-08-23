#region

using Example.PetShop.Domain;
using Microsoft.Practices.Composite.Events;
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
        private readonly IEventAggregator _events;

        public HistoryModule(IRegionManager regionManager, IEventAggregator events, PetRepository petRepository)
        {
            _regionManager = regionManager;
            _events = events;
            _petRepository = petRepository;
        }

        #region IModule Members

        public void Initialize()
        {
            var historyViewModel = new HistoryViewModel(_petRepository, _events);
            _regionManager.Regions["Admin"].Add(new HistoryPanel(historyViewModel));
        }

        #endregion
    }
}