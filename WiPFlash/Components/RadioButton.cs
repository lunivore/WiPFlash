#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class RadioButton : AutomationElementWrapper
    {
        public RadioButton(AutomationElement element) : base(element)
        {
        }

        public bool Selected
        {
            get { return bool.Parse(((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern)).Current.IsSelected.ToString()); }
        }

        public void Select()
        {
            var pattern = ((SelectionItemPattern)Element.GetCurrentPattern(SelectionItemPattern.Pattern));
            pattern.Select();
            
        }
    }
}
