#region

using System.Windows.Automation;
using WiPFlash.Framework;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class ScrollViewer : Container<ScrollViewer>
    {
        private readonly ScrollPatternWrapper _scrollPattern;

        public delegate bool SomethingToScrollTo(ScrollViewer scrollViewer);
        public delegate void FailureToScrollHandler(ScrollViewer scrollViewer);

        private delegate void ScrollAction();
        private delegate bool ScrollPercentCheck();

        public ScrollViewer(AutomationElement element, string name) : base(element, name)
        {
            _scrollPattern = new ScrollPatternWrapper(element);

        }

        public ScrollViewer(AutomationElement element, string name, IFindAutomationElements finder) : base(element, name, finder)
        {
            _scrollPattern = new ScrollPatternWrapper(element);
        }

        public bool ScrollDown(SomethingToScrollTo check, FailureToScrollHandler handler)
        {
            return ScrollUntil(check, handler, _scrollPattern.ScrollDown, () => _scrollPattern.ScrollPercentDown() < 100);
        }

        public bool ScrollUp(SomethingToScrollTo check, FailureToScrollHandler handler)
        {
            return ScrollUntil(check, handler, _scrollPattern.ScrollUp, () => _scrollPattern.ScrollPercentDown() > 0);
        }

        public bool ScrollLeft(SomethingToScrollTo check, FailureToScrollHandler handler)
        {
            return ScrollUntil(check, handler, _scrollPattern.ScrollLeft, () => _scrollPattern.ScrollPercentLeft() < 100);
        }

        public bool ScrollRight(SomethingToScrollTo check, FailureToScrollHandler handler)
        {
            return ScrollUntil(check, handler, _scrollPattern.ScrollRight, () => _scrollPattern.ScrollPercentLeft() > 0);
        }

        private bool ScrollUntil(SomethingToScrollTo check, FailureToScrollHandler handler, ScrollAction action, ScrollPercentCheck percentCheck)
        {
            while (!check(this) && percentCheck())
            {
                action();
            }
            if (!check(this))
            {
                handler(this);
            }
            return !check(this);
        }
    }

    public class ScrollDirection
    {
    }
}