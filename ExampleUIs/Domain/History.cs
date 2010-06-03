#region

using System;
using System.ComponentModel;

#endregion

namespace ExampleUIs.Domain
{
    public class History
    {  
        private string _text = "History so far:" + Environment.NewLine;

        public void AddText(string addition)
        {
            Console.WriteLine("History: {0}", addition);
            _text = _text + addition + Environment.NewLine;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
            }
        }


    }
}