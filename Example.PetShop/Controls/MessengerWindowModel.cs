using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Example.PetShop.Controls
{
    public class MessengerWindowModel
    {
        private readonly string _title;
        private readonly string _message;
        private readonly Window _parent;

        public MessengerWindowModel(string title, string message, Window parent)
        {
            _title = title;
            _message = message;
            _parent = parent;
        }

        public string Message
        {
            get { return _message; }
        }

        public string Title
        {
            get { return _title; }
        }

        public ICommand Close
        {
            get { return new DelegateCommand<string>(result => { _parent.DialogResult = result.Equals("true"); }); }
        }
    }
}