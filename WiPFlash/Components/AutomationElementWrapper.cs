using System;
using System.Windows.Automation;
using WiPFlash.Framework;

namespace WiPFlash.Components
{
    /**
     * Constructs an automation element wrapper which will find other elements by automation id (WPF element Name)
     */
    public abstract class AutomationElementWrapper
    {
        private readonly AutomationElement _element;

        protected AutomationElementWrapper(AutomationElement element)
        {
            if (element == null) {
                throw new NullReferenceException("No element was found for this " + this.GetType().Name);  }
            _element = element;
        }

        public AutomationElement Element
        {
            get { return _element; }
        }
    }
}