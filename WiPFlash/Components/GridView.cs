#region

using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class GridView : AutomationElementWrapper<GridView>
    {
        private readonly GridPatternWrapper _gridPattern;

        public GridView(AutomationElement element, string name) : base(element, name)
        {
            _gridPattern = new GridPatternWrapper(element);
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                   {
                       new StructureChangeEvent(TreeScope.Element),
                       new PropertyChangeEvent(TreeScope.Descendants, AutomationElement.NameProperty)
                   };
            }
        }

        public string[,] AllText
        {
            get { return _gridPattern.AllText; }
        }

        public string TextAt(int column, int row)
        {
            return _gridPattern.TextAt(column, row);
        }

        public bool ContainsRow(params string[] fields)
        {
            var allText = AllText;
            if (allText.GetLength(1) != fields.Length) { return false; }
            var found = false;
            for (int row = 0; row < allText.GetLength(0) && !found; row++)
            {
                found = true;
                for (int col = 0; col < allText.GetLength(1) && found; col++)
                {
                    if (allText[row, col] != fields[col])
                    {
                        found = false;
                    }
                }
            }
            return found;
        }
    }
}
