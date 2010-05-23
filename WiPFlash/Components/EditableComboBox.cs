#region

using System.Windows.Automation;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class EditableComboBox : ComboBox
    {
        private readonly ValuePatternWrapper _valuePattern;

        public EditableComboBox(AutomationElement element, string name) : base(element, name)
        {
            _valuePattern = new ValuePatternWrapper(element);
        }

        public string Text
        {
            get
            {
                return _valuePattern.Value;
            }
            set
            {
                _valuePattern.Value = value;
            }
        }
    }
}
