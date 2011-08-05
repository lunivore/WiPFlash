#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class RadioButton : AutomationElementWrapper
    {
        private readonly SelectionItemPatternWrapper _selectionItemPattern;

        public RadioButton(AutomationElement element, string name) : base(element, name)
        {
            _selectionItemPattern = new SelectionItemPatternWrapper(element);
        }

        public bool Selected
        {
            get { return _selectionItemPattern.Selected; }
        }

        public void Select()
        {
            _selectionItemPattern.Select();
            
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                           {
                                new OrdinaryEvent(SelectionItemPattern.ElementSelectedEvent, TreeScope.Element)
                           };
            }
        }
    }
}
