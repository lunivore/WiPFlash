#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework.Patterns
{
    public class ScrollPatternWrapper
    {
        private readonly AutomationElement _element;

        public ScrollPatternWrapper(AutomationElement element)
        {
            _element = element;
        }

        public void ScrollDown()
        {
            ((ScrollPattern)_element.GetCurrentPattern(ScrollPattern.Pattern)).ScrollVertical(ScrollAmount.LargeIncrement);
        }

        public void ScrollUp()
        {
            ((ScrollPattern)_element.GetCurrentPattern(ScrollPattern.Pattern)).ScrollVertical(ScrollAmount.LargeDecrement);
        }

        public void ScrollLeft()
        {
            ((ScrollPattern)_element.GetCurrentPattern(ScrollPattern.Pattern)).ScrollHorizontal(ScrollAmount.LargeIncrement);
        }

        public void ScrollRight()
        {
            ((ScrollPattern)_element.GetCurrentPattern(ScrollPattern.Pattern)).ScrollHorizontal(ScrollAmount.LargeDecrement);
        }

        public double ScrollPercentDown()
        {
            return ((ScrollPattern) _element.GetCurrentPattern(ScrollPattern.Pattern)).Current.VerticalScrollPercent;
        }

        public double ScrollPercentLeft()
        {
            return ((ScrollPattern)_element.GetCurrentPattern(ScrollPattern.Pattern)).Current.HorizontalScrollPercent;
        }
    }
}
