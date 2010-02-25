using System;
using System.ComponentModel;
using System.Net.Mime;

namespace ExampleUIs.Domain
{
    public class History : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        private string _text = "History so far:" + Environment.NewLine;

        public void AddText(string addition)
        {
            Console.WriteLine("History: {0}", addition);
            _text = _text + addition + Environment.NewLine;
            PropertyChanged(this, new PropertyChangedEventArgs("Text"));
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }


    }
}