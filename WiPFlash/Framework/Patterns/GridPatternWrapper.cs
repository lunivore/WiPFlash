#region

using System.Windows.Automation;

#endregion

namespace WiPFlash.Framework.Patterns
{
    public class GridPatternWrapper
    {
        public GridPatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public AutomationElement Element { get; set; }

        public string[,] AllText
        {
            get
            {
                var pattern = ((GridPattern)Element.GetCurrentPattern(GridPattern.Pattern));
                var rowCount = pattern.Current.RowCount;
                var colCount = pattern.Current.ColumnCount;
                var text = new string[rowCount, colCount];
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        text[row, col] = pattern.GetItem(row, col).GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
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