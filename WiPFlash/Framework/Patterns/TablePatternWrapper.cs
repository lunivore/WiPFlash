#region

using System;
using System.Windows.Automation;
using WiPFlash.Util;

#endregion

namespace WiPFlash.Framework.Patterns
{
    public class TablePatternWrapper
    {
        public TablePatternWrapper(AutomationElement element)
        {
            Element = element;
        }

        public AutomationElement Element { get; set; }

        public string[,] AllText
        {
            get
            {
                var rowCount = Pattern.Current.RowCount;
                var colCount = Pattern.Current.ColumnCount;
                var text = new string[rowCount, colCount];
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        text[row, col] = Pattern.GetItem(row, col).GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
                    }
                }
                return text;
            }
        }

        private TablePattern Pattern
        {
            get { return ((TablePattern) Element.GetCurrentPattern(TablePattern.Pattern)); }
        }

        public string TextAt(int column, int row)
        {
            return Pattern.GetItem(row, column).GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
        }

        public int ColumnWithHeader(string header)
        {
            var headers = Pattern.Current.GetColumnHeaders();
            return CollectionUtils.IndexOf(headers,
                                           h =>
                                           h.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString()
                                             .Equals(header));
        }

        public int RowCount
        {
            get 
            { 
                return Pattern.Current.RowCount;
            }
        }


        public int ColumnCount
        {
            get
            {
                var pattern = ((TablePattern)Element.GetCurrentPattern(TablePattern.Pattern));
                return pattern.Current.ColumnCount;
            }
        }

        public AutomationElement ElementAt(int row, int column)
        {
            return Pattern.GetItem(row, column);
        }
    }
}