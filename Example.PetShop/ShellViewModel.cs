using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Example.PetShop.Utils;
using Microsoft.Practices.Composite.Regions;

namespace Example.PetShop
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

        }

        public TabPresenter[] Tabs
        {
            get
            {
                return new[]
                           {
                               new TabPresenter
                                   {
                                       Name = "History",
                                       TabTo = new TabToCommand(_regionManager, "History")
                                   },
                               new TabPresenter 
                                   {
                                       Name="Basket",
                                       TabTo = new TabToCommand(_regionManager, "Basket")
                                   },
                               new TabPresenter
                                   {
                                       Name="Registration",
                                       TabTo = new TabToCommand(_regionManager, "Registration")
                                   },
                               new TabPresenter
                                   {
                                       Name="Accessories",
                                       TabTo = new TabToCommand(_regionManager, "Accessories")
                                   }
                           };
            }
        }

        public class TabToCommand : ICommand
        {
            private readonly IRegionManager _manager;
            private readonly string _view;

            public TabToCommand(IRegionManager manager, string view)
            {
                _manager = manager;
                _view = view;
            }

            public void Execute(object parameter)
            {
                foreach (var region in _manager.Regions)
                {
                    foreach (var view in region.Views)
                    {
                        var fe = ((FrameworkElement)view);
                        if (fe.DataContext is IHaveATitle && ((IHaveATitle)fe.DataContext).Title.Equals(_view))
                        {
                            region.Activate(view);
                            return;
                        }
                    }
                }
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged = delegate {};
        }

        public class TabPresenter
        {
            public string Name
            {
                get; set;
            }

            public TabToCommand TabTo
            {
                get; set;
            }

            public override string ToString()
            {
                return String.Format("{0}[{1}]", GetType().Name, Name);
            }
        }
    }
}