using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework.Events;

namespace WiPFlash.Components
{
    public class GridView : AutomationElementWrapper<GridView>
    {
        public GridView(AutomationElement element) : base(element)
        {
        }

        public GridView(AutomationElement element, string name) : base(element, name)
        {
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
            get
            {
                var pattern = ((GridPattern) Element.GetCurrentPattern(GridPattern.Pattern));
                var rowCount = pattern.Current.RowCount;
                var colCount = pattern.Current.ColumnCount;
                var text = new string[colCount, rowCount];
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        text[col, row] = pattern.GetItem(row, col).GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
                    }
                }
                return text;
            }
        }

        public string TextAt(int column, int row)
        {
            var pattern = ((GridPattern)Element.GetCurrentPattern(GridPattern.Pattern));
            return pattern.GetItem(row, column).GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
        }
    }
}
