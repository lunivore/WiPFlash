using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.PetShop.Controls;

namespace Example.PetShop.Utils
{
    public class Messenger
    {
        public bool Show(string title, string message)
        {
            bool? result = new MessengerWindow(title, message).ShowDialog();
            return result ?? false;
        }
    }
}
