#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Components
{
    public class EditableComboBox : ComboBox
    {
        public EditableComboBox(AutomationElement element) : base(element)
        {
        }

        public string Text
        {
            get
            {
                return ((ValuePattern)Element.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;
            }
            set
            {
                ((ValuePattern)Element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(value);
            }
        }
    }
}
