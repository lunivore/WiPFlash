using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExampleUIs.BasketModule.View.Model;

namespace ExampleUIs.BasketModule.View
{
    /// <summary>
    /// Interaction logic for BasketPanel.xaml
    /// </summary>
    public partial class BasketPanel : StackPanel
    {
        public BasketPanel(BasketViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
