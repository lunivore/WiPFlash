using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.PetShop.Utils;

namespace Example.PetShop.Controls
{
    public class Messenger : IHandleMessages
    {
        public bool Show(string title, string message)
        {
            bool? result = new MessengerWindow(title, message).ShowDialog();
            return result ?? false;
        }
    }
}