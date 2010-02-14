#region

using System;
using System.Windows.Automation;

#endregion

namespace WiPFlash.Component
{
    public class Window
    {
        private readonly AutomationElement _element;

        public Window(AutomationElement element)
        {
            _element = element;
        }

        public AutomationElement AutomationElement
        {
            get { return _element; }
        }
    }
}
