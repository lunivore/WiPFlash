using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Example.PetShop.Controls
{
    /// <summary>
    /// Interaction logic for Messenger.xaml
    /// </summary>
    public partial class MessengerWindow : Window
    {
        public MessengerWindow(string title, string message)
        {
            InitializeComponent();
            DataContext = new MessengerWindowModel(title, message, this);
        }
    }
}